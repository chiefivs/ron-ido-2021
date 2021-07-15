using System;
using System.Linq;

namespace Ron.Ido.BM.Models.OData
{
    public class ODataCustomOrder<TEntity>
    {
        public string Field { get; private set; }

        public Func<IQueryable<TEntity>, IQueryable<TEntity>> AscOrder { get; private set; }

        public Func<IQueryable<TEntity>, IQueryable<TEntity>> DescOrder { get; private set; }

        public ODataCustomOrder(string field, Func<IQueryable<TEntity>, IQueryable<TEntity>> ascOrder, Func<IQueryable<TEntity>, IQueryable<TEntity>> descOrder)
        {
            Field = field;
            AscOrder = ascOrder;
            DescOrder = descOrder;
        }
    }
}
