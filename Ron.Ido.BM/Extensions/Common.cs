using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Extensions
{
    public static class Common
    {
        public static bool In<T>(this T instance, params T[] pars) where T : IComparable
        {
            return pars.Contains(instance);
        }
    }
}
