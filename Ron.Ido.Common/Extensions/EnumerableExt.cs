using System.Collections.Generic;
using System.Linq;

namespace Ron.Ido.Common.Extensions
{
	public static class EnumerableExt
	{
		public static bool IsNullOrEmpty<T>( this IEnumerable<T> items )
		{
			return items == null || !items.Any();
		}
	}
}
