﻿using Ron.Ido.Common.Attributes;
using Ron.Ido.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ron.Ido.BM.Models.OData
{
    [TypeScriptModule("odata")]
    public class ODataRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public IEnumerable<ODataFilter> Filters { get; set; }
        public IEnumerable<ODataOrder> Orders { get; set; }

        public ODataOrder GetOrder(string field)
        {
            return Orders?.FirstOrDefault(i => i.Field.FromCamel() == field.FromCamel());
        }

        public void ReplaceOrder(string srcField, params string[] dstField)
        {
            if (Orders == null)
                return;

            var srcOrder = GetOrder(srcField);
            if (srcOrder == null)
                return;

            Orders = dstField.Select(f => new ODataOrder { Field = f, Direct = srcOrder.Direct });

        }

        public Func<IQueryable<TEntity>, IQueryable<TEntity>> CreateCustomFilter<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> func)
        {
            return func;
        }

        public Func<IQueryable<TEntity>, IQueryable<TEntity>> CreateMultiselectFilter<TEntity>(string field, Func<TEntity, IEnumerable<long>> idSelector)
        {
            return CreateCustomFilter<TEntity>(query => {
                var filter = Filters.FirstOrDefault(f => f.Field.ToCamel() == field.ToCamel());
                if(filter != null && filter.Values.Any())
                {
                    var ids = filter.Values.Select(v => v.Parse<long>(0)).ToArray();

                    //Expression<Func<TEntity, IEnumerable<long>>> idExpr = a => idSelector(a);
                    //var boolExpr = Expression.Condition(idExpr, )

                    //var list = query
                    //    .Where(expr);
                }

                return query;
            });
        }

    }
}
