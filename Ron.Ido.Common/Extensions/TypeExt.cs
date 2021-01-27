using System;
using System.Reflection;

namespace Ron.Ido.Common.Extensions
{
	public static class TypeExt
	{
		/// <summary>
		/// Возвращает тип свойства из указанного типа. Разрешается использование цепочек имен
		/// вроде City.Country.Name
		/// </summary>
		/// <param name="type">Тип объекта</param>
		/// <param name="name">Имя свойства</param>
		/// <returns></returns>
		public static Type GetPropertyType(this Type type, string name)
		{
			string[] parts = name.Split(new[] { '.' }, 2);
			PropertyInfo pi = type.GetProperty(parts[0]);
			if (pi == null)
				return null;

			if (parts.Length == 1)
				return pi.PropertyType;

			return GetPropertyType(pi.PropertyType, parts[1]);
		}
	}
}
