using Ron.Ido.EM;
using System;
using System.Linq;

namespace Ron.Ido.Migrator.Base
{
    public static class AppDbContextExt 
    {
        public static void AddEntityIfNotExists<T>(this AppDbContext context, T entity, Func<T, bool> condition) where T:class
        {
            if (context.Set<T>().Any(condition))
                return;

            context.Add(entity);
        }
    }
}
