using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.Common;
using Ron.Ido.Common.DependencyInjection;
using Ron.Ido.Common.Extensions;
using Ron.Ido.DbStorage;
using Ron.Ido.EM;
using Ron.Ido.FileStorage;
using Ron.Ido.Web.Authorization;
using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Security.Authentication;
using System.Text.Json;

namespace ForeignDocsRec2020.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private const string DBCONTEXT_CREATED = "AppDbContextCreated";
        private const string APPSETTINGS = "appsettings.json";

        public Startup()
        {
            string basepath = Environment.GetEnvironmentVariable(Constants.ConfigFolderPath)
                  ?? Environment.GetEnvironmentVariable(Constants.ConfigFolderPath, EnvironmentVariableTarget.Machine);

            var builder = new ConfigurationBuilder();
            if (File.Exists(APPSETTINGS))
                builder.AddJsonFile(APPSETTINGS, true);
            else if(!string.IsNullOrEmpty(basepath))
                builder.AddJsonFile(Path.Combine(basepath, APPSETTINGS), true);

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dbContextSettings = Configuration.GetSettings<AppDbContextSettings>();
            services.AddAppDbContext(dbContextSettings, (provider, context) => {
                var httpcontext = provider.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
                //  если это httprequest, то при создании контекста сразу открываем транзакцию
                //  (при завершении запроса закроем)

                //  (при завершении запроса закроем)
                if (httpcontext != null)
                {
                    httpcontext.Items.Add(DBCONTEXT_CREATED, true);
                    context.BeginTransaction();
                }
            });

            var authSettings = Configuration.GetSettings<AuthOptionSettings>();
            AuthOptions.SetSettings(authSettings);

            services.AddMvcCore();
            services
                .AddHttpContextAccessor()
                .AddFileStorage<Ron.Ido.EM.Entities.FileInfo>()
                .AddMediatR(typeof(Ron.Ido.BM.IAssemblyMarker), typeof(Startup))
                .AddDependencies(typeof(Ron.Ido.BM.IAssemblyMarker), typeof(Startup))
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = false,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = false,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = false,
                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = false,
                    };
                });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RonIdo2021.Web", Version = "v1" });
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
                c.OperationFilter<SwaggerAuthorizationFilter>();
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ron Ido Web v1"));
            }

            //app.UseResponseCompression();

            app.Use(async (context, next) =>
            {
                await next();
                //  это аналог endrequest
                //  если в процессе обработки запроса создавался AppDbContext, то нужно закрыть транзакцию
                if (context.Items.ContainsKey(DBCONTEXT_CREATED))
                {
                    var dbcontext = context.RequestServices.GetService<AppDbContext>();
                    if (context.Response.StatusCode == (int)HttpStatusCode.OK)
                        //  при успешном результате коммитим изменения в БД
                        dbcontext?.Commit();
                    else
                        //  иначе отменяем их
                        dbcontext?.Rollback();
                }
            });

            app.UseExceptionHandler(conf =>
            {
                conf.Run(async context =>
                {
                    var handler = context.Features.Get<IExceptionHandlerPathFeature>();
                    var error = handler?.Error;

                    if (error == null)
                        return;

                    if (error is AuthenticationException)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync(error.Message);
                        return;

                    }

                    if (error is UnauthorizedAccessException)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        return;
                    }

                    if (error is ODataValidationException)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsJsonAsync((error as ODataValidationException).Errors);
                        return;
                    }

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync(error.Message);
                });
            });

            app.UseAuthentication();
            app.UseRouting();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Use(async (context, next) =>
            {
                if (string.IsNullOrEmpty(context.Request.Path.Value.Trim('/')))
                {
                    context.Request.Path = "/index.html";
                }

                await next();
            });
            app.UseStaticFiles();

        }
    }

    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string actionName = context.ActionDescriptor.DisplayName;
            string exceptionStack = context.Exception.StackTrace;
            string exceptionMessage = context.Exception.Message;
            context.Result = new ContentResult
            {
                Content = $"В методе {actionName} возникло исключение: \n {exceptionMessage} \n {exceptionStack}"
            };
            context.ExceptionHandled = true;
        }
    }
}
