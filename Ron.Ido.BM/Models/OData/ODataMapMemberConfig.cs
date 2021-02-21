using AutoMapper;
using System;
using System.Linq.Expressions;

namespace Ron.Ido.BM.Models.OData
{
    public class ODataMapMemberConfig<TEntity, TDto>
    {
        public Expression<Func<TDto, object>> DestinationMember { get; private set; }
        public Action<IMemberConfigurationExpression<TEntity, TDto, object>> MemberOptions { get; private set; }

        public ODataMapMemberConfig(
            Expression<Func<TDto, object>> getDestinationMemberFunc,
            Action<IMemberConfigurationExpression<TEntity, TDto, object>> memberMapExprAction
        )
        {
            DestinationMember = getDestinationMemberFunc;
            MemberOptions = memberMapExprAction;
        }
    }
}
