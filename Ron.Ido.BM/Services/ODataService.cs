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
            if (filters != null)
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
                        case ODataFilterTypeEnum.Contains:
                            query = query.WhereContains(propInfo.Name, filter.Values.First());
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
    }
}
