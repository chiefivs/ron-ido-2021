namespace Ron.Ido.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;

    public static class StringExt
    {
        public static string ToCamel(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;

            return $"{source.Substring(0,1).ToLower()}{source.Substring(1)}";
        }

        public static string FromCamel(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;

            return $"{source.Substring(0, 1).ToUpper()}{source.Substring(1)}";
        }

        public static T Parse<T>(this string source, T defValue)
        {
            try
            {
                return (T)source.Parse(typeof(T));
            }
            catch
            {
                return defValue;
            }
        }

        // ReSharper disable once InconsistentNaming
        private static readonly Dictionary<Type, MethodInfo> _parseMethods = new Dictionary<Type, MethodInfo>();
        public static object Parse(this string source, Type type)
        {
            if (type == typeof(string))
                return source;

            if (type == typeof(double))
            {

                if (double.TryParse(source, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                    return result;
                return null;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                type = type.GenericTypeArguments.First();

            if (type.IsEnum)
            {
                Enum.TryParse(type, source, out object outData);
                return outData;
            }

            MethodInfo parseMtd;
            lock (_parseMethods)
            {
                if (!_parseMethods.ContainsKey(type))
                    _parseMethods[type] = type.GetMethod("Parse", new[] { typeof(string) });

                parseMtd = _parseMethods[type];
            }

            if (parseMtd == null)
                return null;

            try
            {
                return parseMtd.Invoke(null, new object[] { source });
            }
            catch
            {
                return null;
            }
        }

        public static string MaxLen(this string source, int maxlen)
        {
            return source.Length <= maxlen ? source : source.Substring(0, maxlen);
        }

        //private const string _salt = "1E1576DCDF95EB152E27C7A5D7FFC";
        public static string GetHashString(this string source)
        {
            using (var md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(source));
                var builder = new StringBuilder();
                foreach (byte t in data)
                {
                    builder.Append(t.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public static string Join(this IEnumerable<string> list, string separator) => string.Join(separator, list);


        public static string FormatDate(this DateTime self)
        {
            return self.ToString("dd.MM.yyyy HH:mm");
        }
        public static string FormatDate(this DateTime? self)
        {
            if ( !self.HasValue )
                return string.Empty;
            return self.Value.FormatDate();
        }
    }
}
