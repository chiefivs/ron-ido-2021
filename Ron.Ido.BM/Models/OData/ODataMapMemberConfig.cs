using AutoMapper;
using System;
using System.Linq.Expressions;

namespace Ron.Ido.BM.Models.OData
{
    public class ODataMapMemberConfig<TSource, TDestination>
    {
        public Expression<Func<TDestination, object>> DestinationMember { get; private set; }
        public Action<IMemberConfigurationExpression<TSource, TDestination, object>> MemberOptions { get; private set; }

        public ODataMapMemberConfig(
            Expression<Func<TDestination, object>> getDestinationMemberFunc,
            Action<IMemberConfigurationExpression<TSource, TDestination, object>> memberMapExprAction
        )
        {
            DestinationMember = getDestinationMemberFunc;
            MemberOptions = memberMapExprAction;
        }
    }
}
