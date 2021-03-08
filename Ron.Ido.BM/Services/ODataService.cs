using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.Common.DependencyInjection;
using Ron.Ido.Common.Extensions;
using Ron.Ido.EM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ron.Ido.BM.Services
{
    public class ODataService: IDependency
    {
        private AppDbContext _appDbContext;

        public ODataService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
            
            var query = _appDbContext.Set<TEntity>().AsQueryable();
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
            var query = _appDbContext.Set<TEntity>().AsQueryable();
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
    }
}
