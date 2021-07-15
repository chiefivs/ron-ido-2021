using System;
using System.Linq;

namespace Ron.Ido.BM.Models.OData
{
    public class ODataCustomFilter<TEntity>
    {
        public string Field { get; private set; }

        public bool IsStatic { get; set; }

        public Func<IQueryable<TEntity>, string[], IQueryable<TEntity>> Filter { get; private set; }

        public ODataCustomFilter(string field, Func<IQueryable<TEntity>, string[], IQueryable<TEntity>> filter, bool isStatic = false)
        {
            Field = field;
            Filter = filter;
            IsStatic = isStatic;
        }
    }
}
