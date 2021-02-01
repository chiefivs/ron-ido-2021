using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ron.Ido.Common;
using Ron.Ido.Common.DependencyInjection;
using Ron.Ido.Common.Extensions;
using Ron.Ido.DbStorage;
using Ron.Ido.EM;
using Ron.Ido.Web.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Authentication;

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
            builder.AddJsonFile(!string.IsNullOrEmpty(basepath) ? Path.Combine(basepath, APPSETTINGS) : APPSETTINGS, true, true);

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
                .AddMediatR(typeof(Ron.Ido.BM.IAssemblyMarker), typeof(Startup))
                .AddDependencies(typeof(Ron.Ido.BM.IAssemblyMarker), typeof(Startup))
                                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироватьс€ издатель при валидации токена
                        ValidateIssuer = false,
                        // строка, представл€юща€ издател€
                        ValidIssuer = AuthOptions.ISSUER,
                        // будет ли валидироватьс€ потребитель токена
                        ValidateAudience = false,
                        // установка потребител€ токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироватьс€ врем€ существовани€
                        ValidateLifetime = false,
                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидаци€ ключа безопасности
                        ValidateIssuerSigningKey = false,
                    };
                });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RonIdo2021.Web", Version = "v1" });
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
            app.UseExceptionHandler(conf =>
            {
                conf.Run(async context => {
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

                    //var factory = app.ApplicationServices.GetService<ILoggerFactory>();
                    //var logger = factory.CreateLogger("Web application");
                    //var logItem = new LogItem
                    //{
                    //    Message = "¬нутренн€€ ошибка сервиса  URL=" + Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.Request)
                    //};

                    //var uid = Guid.NewGuid();
                    //logger.LogError(uid, logItem, error);

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    //context.Response.ContentType = "application/json";
                    //await context.Response.WriteAsync($"{{\"LogItemUid\":\"{uid}\"}}");
                    await context.Response.WriteAsync(error.Message);
                });
            });

            app.UseAuthentication();
            app.UseRouting();

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

            app.Use(async (context, next) =>
            {
                await next();
                //  это аналог endrequest
                //  если в процессе обработки запроса создавалс€ AppDbContext, то нужно закрыть транзакцию
                if (context.Items.ContainsKey(DBCONTEXT_CREATED))
                {
                    var dbcontext = context.RequestServices.GetService<AppDbContext>();
                    if (context.Response.StatusCode == (int)HttpStatusCode.OK)
                        //  при успешном результате коммитим изменени€ в Ѕƒ
                        dbcontext?.Commit();
                    else
                        //  иначе отмен€ем их
                        dbcontext?.Rollback();
                }
            });

        }
    }
}
