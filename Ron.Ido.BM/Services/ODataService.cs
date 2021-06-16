using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.Common.DependencyInjection;
using Ron.Ido.Common.Extensions;
using Ron.Ido.EM;
using Ron.Ido.EM.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ron.Ido.BM.Services
{

	public class ODataService: IDependency
    {
        protected AppDbContext AppDbContext;

        public ODataService(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public ODataPage<TDto> GetPage<TEntity, TDto>(
            ODataRequest request,
            IEnumerable<ODataMapMemberConfig<TEntity, TDto>> memberConfigs) where TDto : class where TEntity : class
        {
            return GetPage(request, null, memberConfigs);
        }

        public ODataPage<TDto> GetPage<TEntity, TDto>(
            ODataRequest request,
            IEnumerable<Func<IQueryable<TEntity>, IQueryable<TEntity>>> customFilters = null,
            IEnumerable<ODataMapMemberConfig<TEntity, TDto>> memberConfigs = null) where TDto:class where TEntity:class
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                var expr = cfg.CreateMap<TEntity, TDto>();

                if(memberConfigs != null)
                {
                    foreach(var config in memberConfigs)
                    {
                        expr = expr.ForMember(config.DestinationMember, config.MemberOptions);
                    }
                }
            });
            
            var query = AppDbContext.Set<TEntity>().AsQueryable();
            query = ApplyFilters(query, request.Filters, customFilters);
            query = ApplyOrders(query, request.Orders);
            var items = query.Skip(request.Skip).Take(request.Take).ProjectTo<TDto>(mapperConfig);

            return new ODataPage<TDto>
            {
                Items = items.ToArray(),
                Size = items.Count(),
                Skip = request.Skip,
                Total = query.Count()
            };
        }

        public ODataPage<TEntity> GetPage<TEntity>(
            ODataRequest request,
            IEnumerable<Func<IQueryable<TEntity>, IQueryable<TEntity>>> customFilters = null) where TEntity : class
        {
            var query = AppDbContext.Set<TEntity>().AsQueryable();
            query = ApplyFilters(query, request.Filters, customFilters);
            var items = query.Skip(request.Skip).Take(request.Take);

            return new ODataPage<TEntity>
            {
                Items = items.ToArray(),
                Size = items.Count(),
                Skip = request.Skip,
                Total = query.Count()
            };
        }

        public IEnumerable<ODataOption> GetOptions<TEntity>(string textPropName, string valuePropName, Func<IQueryable<TEntity>, IQueryable<TEntity>> optionsFilter = null) where TEntity : class
        {
            return GetOptions(textPropName, valuePropName, null, null, optionsFilter);
        }

        public IEnumerable<ODataOption> GetOptions<TEntity>(string textPropName, string valuePropName, DateTime? date, Func<IQueryable<TEntity>, IQueryable<TEntity>> optionsFilter = null) where TEntity : class
        {
            return GetOptions(textPropName, valuePropName, null, date, optionsFilter);
        }

        public IEnumerable<ODataOption> GetOptions<TEntity>(string textPropName, string valuePropName, string parentPropName, Func<IQueryable<TEntity>, IQueryable<TEntity>> optionsFilter = null) where TEntity : class
        {
            return GetOptions(textPropName, valuePropName, parentPropName, null, optionsFilter);
        }

        public IEnumerable<ODataOption> GetOptions<TEntity>(string textPropName, string valuePropName, string parentPropName, DateTime? date = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> optionsFilter = null) where TEntity : class
        {
            var query = AppDbContext.Set<TEntity>().AsQueryable();
            if (typeof(IDateDependent).IsAssignableFrom(typeof(TEntity)) && date.HasValue)
                query = query.Where(i => (
               (date >= ((IDateDependent)i).BeginDate) || ((IDateDependent)i).BeginDate == null)
               && ((date <= ((IDateDependent)i).EndDate) || ((IDateDependent)i).EndDate == null));

            if (optionsFilter != null)
                query = optionsFilter(query);

            if (typeof(IOrdered).IsAssignableFrom(typeof(TEntity)))
                query = query.OrderBy(i => ((IOrdered)i).OrderNum);

            string valPropName = string.IsNullOrEmpty(valuePropName)
                ? textPropName
                : valuePropName;

            var arr = query.ToArray();

            return arr.Select(i => new ODataOption
            {
                Text = i.GetPropertyValue(textPropName).ToString(),
                Value = i.GetPropertyValue(valPropName),
                Parent = string.IsNullOrEmpty(parentPropName) ? null : i.GetPropertyValue(parentPropName)
            });
        }

        public TDto GetDto<TEntity, TDto>(
            long id,
            IEnumerable<ODataMapMemberConfig<TEntity, TDto>> memberConfigs = null) where TEntity:class, new()
        {
            var entity = AppDbContext.Find<TEntity>(id) ?? new TEntity();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                var expr = cfg.CreateMap<TEntity, TDto>();

                if (memberConfigs != null)
                {
                    foreach (var config in memberConfigs)
                    {
                        expr = expr.ForMember(config.DestinationMember, config.MemberOptions);
                    }
                }
            });

            var mapper = new Mapper(mapperConfig);

            return mapper.Map<TDto>(entity);
        }

        public Dictionary<string, List<string>> ValidateDto<TDto>(
            TDto dto,
            Func<TDto, AppDbContext, IEnumerable<ValidationResult>> validationInDb = null)
        {
            var dtoValidationResults = new List<ValidationResult>();
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(dto);
            Validator.TryValidateObject(dto, context, dtoValidationResults);

            var validable = dto as IValidatableObject;
            if(validable != null)
                dtoValidationResults.AddRange(validable.Validate(context));

            if (validationInDb != null)
                dtoValidationResults.AddRange(validationInDb(dto, AppDbContext));

            var results = new Dictionary<string, List<string>>();
            foreach(var dtoRes in dtoValidationResults)
            {
                foreach(var memberName in dtoRes.MemberNames)
                {
                    var field = memberName.ToCamel();
                    if (!results.ContainsKey(field))
                        results[field] = new List<string>();

                    if(!results[field].Contains(dtoRes.ErrorMessage))
                        results[field].Add(dtoRes.ErrorMessage);
                }
            }

            return results;
        }

        public void SaveDto<TDto, TEntity>(
            TDto dto,
            Action<TDto, TEntity, AppDbContext> customize) where TEntity:class, new()
        {
            SaveDto(dto, null, customize);
        }

        public void SaveDto<TDto, TEntity>(
            TDto dto,
            IEnumerable<ODataMapMemberConfig<TDto, TEntity>> memberConfigs = null,
            Action<TDto, TEntity, AppDbContext> customize = null) where TEntity:class, new()
        {
            var keyNames = _getKeyNames(typeof(TEntity));
            var keys = keyNames.Select(key => dto.GetPropertyValue(key)).ToArray();
            var entity = AppDbContext.Find<TEntity>(keys);
            if(entity == null)
            {
                entity = new TEntity();
                AppDbContext.Add(entity);
            }

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                var expr = cfg.CreateMap<TDto, TEntity>();
                foreach(var keyName in keyNames)
                {
                    expr.ForMember(keyName, opt => opt.Ignore());
                }

                if (memberConfigs != null)
                {
                    foreach (var config in memberConfigs)
                    {
                        expr = expr.ForMember(config.DestinationMember, config.MemberOptions);
                    }
                }
            });

            var mapper = new Mapper(mapperConfig);
            mapper.Map(dto, entity);
            if (customize != null)
                customize(dto, entity, AppDbContext);

            AppDbContext.SaveChanges();
        }

        private IQueryable<TEntity> ApplyFilters<TEntity>(
            IQueryable<TEntity> query,
            IEnumerable<ODataFilter> filters,
            IEnumerable<Func<IQueryable<TEntity>, IQueryable<TEntity>>> customFilters) where TEntity : class
        {
            if (filters != null && filters.Any())
            {
                var properties = typeof(TEntity).GetProperties();

                foreach (var filter in filters)
                {
                    var propInfo = properties.FirstOrDefault(p => p.Name.ToCamel() == filter.Field.ToCamel());
                    if (propInfo == null)
                        continue;

                    switch (filter.Type)
                    {
                        case ODataFilterTypeEnum.Equals:
                            query = query.WhereEqual(propInfo.Name, filter.Values.First().Parse(propInfo.PropertyType));
                            break;
                        case ODataFilterTypeEnum.NotEquals:
                            query = query.WhereNotEqual(propInfo.Name, filter.Values.First().Parse(propInfo.PropertyType));
                            break;
                        case ODataFilterTypeEnum.LessThan:
                            query = query.WhereLessThan(propInfo.Name, filter.Values.First().Parse(propInfo.PropertyType));
                            break;
                        case ODataFilterTypeEnum.GreatThan:
                            query = query.WhereGreaterThan(propInfo.Name, filter.Values.First().Parse(propInfo.PropertyType));
                            break;
                        case ODataFilterTypeEnum.LessThanOrEqual:
                            query = query.WhereLessThanOrEqual(propInfo.Name, filter.Values.First().Parse(propInfo.PropertyType));
                            break;
                        case ODataFilterTypeEnum.GreatThanOrEqual:
                            query = query.WhereGreaterThanOrEqual(propInfo.Name, filter.Values.First().Parse(propInfo.PropertyType));
                            break;
                        case ODataFilterTypeEnum.In:
                            if(propInfo.PropertyType == typeof(int))
                            {
                                query = query.WhereContains(propInfo.Name, filter.Values.Select( v => v.Parse(0)));
                            }
                            else if (propInfo.PropertyType == typeof(long))
                            {
                                query = query.WhereContains(propInfo.Name, filter.Values.Select(v => v.Parse<long>(0)));
                            }
                            //else if (propInfo.PropertyType.IsGenericType && propInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                            //{
                            //    var idProp = _getMtMOtherKeyName(typeof(TEntity), propInfo.Name);
                            //    var ids = filter.Values.Select(v => v.Parse<long>(0));
                            //    query = query.WhereContains($"{propInfo.Name}.{idProp}", ids);
                            //}
                            break;
                        case ODataFilterTypeEnum.BetweenNone:
                            if(filter.Values.Length == 2)
                            {
                                query = query
                                    .WhereGreaterThan(propInfo.Name, filter.Values[0].Parse(propInfo.PropertyType))
                                    .WhereLessThan(propInfo.Name, filter.Values[1].Parse(propInfo.PropertyType));
                            }
                            break;
                        case ODataFilterTypeEnum.BetweenLeft:
                            if (filter.Values.Length == 2)
                            {
                                query = query
                                    .WhereGreaterThanOrEqual(propInfo.Name, filter.Values[0].Parse(propInfo.PropertyType))
                                    .WhereLessThan(propInfo.Name, filter.Values[1].Parse(propInfo.PropertyType));
                            }
                            break;
                        case ODataFilterTypeEnum.BetweenRight:
                            if (filter.Values.Length == 2)
                            {
                                query = query
                                    .WhereGreaterThan(propInfo.Name, filter.Values[0].Parse(propInfo.PropertyType))
                                    .WhereLessThanOrEqual(propInfo.Name, filter.Values[1].Parse(propInfo.PropertyType));
                            }
                            break;
                        case ODataFilterTypeEnum.BetweenAll:
                            if (filter.Values.Length == 2)
                            {
                                query = query
                                    .WhereGreaterThanOrEqual(propInfo.Name, filter.Values[0].Parse(propInfo.PropertyType))
                                    .WhereLessThanOrEqual(propInfo.Name, filter.Values[1].Parse(propInfo.PropertyType));
                            }
                            break;
                        case ODataFilterTypeEnum.Starts:
                            break;
                        case ODataFilterTypeEnum.Contains:
                            query = query.WhereContains(propInfo.Name, filter.Values.First(), filter.Aliases);
                            break;
                    }
                }
            }

            if(customFilters != null)
            {
                foreach(var filter in customFilters)
                {
                    query = filter(query);
                }
            }

            return query;
        }

        private IQueryable<TEntity> ApplyOrders<TEntity>(
            IQueryable<TEntity> query,
            IEnumerable<ODataOrder> orders) where TEntity: class
        {
            if(orders != null && orders.Any())
            {
                var isOrdered = false;
                foreach(var order in orders)
                {
                    if(!isOrdered)
                    {
                        if (order.Direct == ODataOrderTypeEnum.Asc)
                            query = query.OrderBy(order.Field.FromCamel());
                        else
                            query = query.OrderByDescending(order.Field.FromCamel());

                        isOrdered = true;
                    }
                    else
                    {
                        if (order.Direct == ODataOrderTypeEnum.Asc)
                            query = query.ThenBy(order.Field.FromCamel());
                        else
                            query = query.ThenByDescending(order.Field.FromCamel());
                    }
                }
            }

            return query;
        }

        private IEnumerable<string> _getKeyNames(Type entityType)
        {
            if (entityType.IsInterface)
            {
                var keyProp = entityType.GetProperties().First(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Any());
                return new[] { keyProp.Name };
            }

            return AppDbContext.Model.FindEntityType(entityType.FullName).FindPrimaryKey().Properties.Select(p => p.Name);
        }

        protected string _getMtMOtherKeyName(Type entityType, string propName)
        {
            var entityDbType = AppDbContext.Model.FindEntityType(entityType.FullName);
            var propDbType = entityDbType.FindNavigation(propName);
            var relDbType = propDbType.ForeignKey.DeclaringEntityType;
            var keys = relDbType.GetForeignKeys().Where(k => k.PrincipalEntityType != entityDbType);

            if (keys.Any())
                return keys.First().Properties.First().Name;

            //  вторая сторона отношения не является foreign key, пытаемся найти ее по первичному ключу
            var mtmPrimaryKey = relDbType.GetForeignKeys().First(k => k.PrincipalEntityType == entityDbType);
            string mtmPrimaryKeyName = mtmPrimaryKey.Properties.First().Name;
            var propType = entityType.GetPropertyType(propName).GetGenericArguments().First();
            var primaryKeyNames = _getKeyNames(propType).Where(k => k != mtmPrimaryKeyName);
            if (primaryKeyNames.Any())
                return primaryKeyNames.First();

            throw new Exception($"Тип {entityType.FullName} является отношением many-to many, но не имеет ни внешнего ключа, ни первичного индекса по полям");
        }

    }
}
