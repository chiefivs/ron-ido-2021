using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Ron.Ido.Common.Extensions
{
    public static class ObjectExt
    {
        // ReSharper disable once InconsistentNaming
        private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> _typePropsInfo = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
        /// <summary>
        /// Возвращает информацию о свойстве объекта. Если свойство не найдено, возвращается null
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name">Имя свойства объекта</param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo(this Object obj, string name)
        {
            PropertyInfo pi;
            lock (_typePropsInfo)
            {
                Type type = obj.GetType();
                if (_typePropsInfo.ContainsKey(type))
                {
                    if (_typePropsInfo[type].ContainsKey(name))
                        return _typePropsInfo[type][name];

                    pi = type.GetProperty(name);
                    if (pi != null)
                        _typePropsInfo[type][name] = pi;
                    return pi;
                }

                _typePropsInfo[type] = new Dictionary<string, PropertyInfo>();
                pi = type.GetProperty(name);
                if (pi != null)
                    _typePropsInfo[type][name] = pi;

                return pi;
            }
        }

        /// <summary>
        /// Возвращает значение свойства объекта по имени. Разрешается использование цепочек имен
        /// вроде City.Country.Name
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name">Имя свойства</param>
        /// <returns></returns>
        public static object GetPropertyValue(this Object obj, string name)
        {
            if (obj == null)
                return null;

            string[] parts = name.Split(new[] { '.' }, 2);
            PropertyInfo pi = obj.GetPropertyInfo(parts[0]);

            if (pi == null)
                return null;

            object res = pi.GetValue(obj, null);
			//System.Diagnostics.Debug.Write( $"({obj}).{name} = {res}\r\n" );

			if ( res == null || parts.Length == 1)
                return res;

            return GetPropertyValue(res, parts[1]);
        }

        /// <summary>
        /// Устанавливает значение свойства объекта по имени. Разрешается использование цепочек имен
        /// вроде City.Country.Name
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        public static void SetPropertyValue(this Object obj, string name, object value)
        {
            if (obj == null)
                return;

            var parts = name.Split(new[] { '.' });
            var prop = parts.Last();

            var target = obj;
            if (parts.Length > 1)
            {
                Array.Resize(ref parts, parts.Length - 1);
                target = GetPropertyValue(obj, string.Join(".", parts));
            }

            var pi = GetPropertyInfo(target, prop);
            pi.SetValue(target, value);
        }

        /// <summary>
        /// Возвращает строковое представление свойства Name,
        /// если оно есть. В противном случае возвращает ToString()
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string DisplayName(this Object obj)
        {
            if (obj == null)
                return "";

            var pi = obj.GetType().GetProperties()
                .FirstOrDefault(p => p.GetCustomAttribute<DisplayNameAttribute>() != null);
            if (pi != null)
                return pi.GetValue(obj, null).ToString();

            pi = obj.GetType().GetProperty("Name");
            if (pi != null)
                return pi.GetValue(obj, null).ToString();

            if (obj is DateTime)
                return ((DateTime)obj).ToShortDateString();

            if (obj is bool)
                return (bool)obj ? "Да" : "Нет";

            return obj.ToString();
        }
    }
}
