using Ron.Ido.EM.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ron.Ido.Importer.NDB.Classes
{
    public class Grant
    {
        public string Group = "";
        public string Description = "";
        public string Value = "";
        public PermissionEnum Permission;
        public int OrderNum = 0;

        public Grant(string description, string group, int ordernum, string value, PermissionEnum perm)
        {
            Group = group;
            Description = description;
            OrderNum = ordernum;
            Value = value;
            Permission = perm;
        }
    }

    public abstract class GrantsUtility
    {
        private static byte[] ToBytes(string value)
        {
            if (value.Length % 2 > 0)
                value = '0' + value;

            byte[] result = new byte[value.Length / 2];
            for (int n = 0; n < value.Length; n += 2)
                result[n / 2] = Byte.Parse(value.Substring(n, 2), NumberStyles.HexNumber);

            Array.Reverse(result);
            return result;
        }

        private static string FromBytes(byte[] value)
        {
            string res = "";
            for (int n = 0; n < value.Length; n++)
                res = value[n].ToString("X2") + res;

            while (res.Length > 0 && res[0] == '0')
                res = res.Substring(1);

            return res;
        }

        private static byte[] Combine(byte[] grant1, byte[] grant2)
        {
            int len = Math.Max(grant1.Length, grant2.Length);
            byte[] result = new byte[len];
            for (int n = 0; n < len; n++)
            {
                byte val1 = (byte)(grant1.Length > n ? grant1[n] : grant1.Length > 0 ? 0x00 : 0xFF);
                byte val2 = (byte)(grant2.Length > n ? grant2[n] : grant2.Length > 0 ? 0x00 : 0xFF);
                result[n] = (byte)(val1 | val2);
            }

            return result;
        }

        private static byte[] Intersect(byte[] grant1, byte[] grant2)
        {
            int len = Math.Max(grant1.Length, grant2.Length);
            byte[] result = new byte[len];
            for (int n = 0; n < len; n++)
            {
                byte val1 = (byte)(grant1.Length > n ? grant1[n] : grant1.Length > 0 ? 0x00 : 0xFF);
                byte val2 = (byte)(grant2.Length > n ? grant2[n] : grant2.Length > 0 ? 0x00 : 0xFF);
                result[n] = (byte)(val1 & val2);
            }

            return result;
        }

        private static byte[] Subtract(byte[] grant1, byte[] grant2)
        {
            int len = Math.Max(grant1.Length, grant2.Length);
            byte[] result = new byte[len];
            for (int n = 0; n < len; n++)
            {
                byte val1 = (byte)(grant1.Length > n ? grant1[n] : grant1.Length > 0 ? 0x00 : 0xFF);
                byte val2 = (byte)(grant2.Length > n ? grant2[n] : grant2.Length > 0 ? 0x00 : 0xFF);
                result[n] = (byte)(val1 & ~val2);
            }

            return result;
        }

        private static bool AnyFlag(byte[] grant)
        {
            return grant.Any(val => val > 0);
        }

        private static string Normalize(string value, int maxlen)
        {
            if (maxlen % 2 > 0)
                maxlen++;

            if (value.ToUpper() == "ALL")
                return new string('F', maxlen);

            char[] result = (new string('0', maxlen)).ToCharArray();
            value.CopyTo(0, result, maxlen - value.Length, value.Length);
            return new string(result);
        }

        /// <summary>
        /// Объединение битовых наборов
        /// </summary>
        public static string Combine(params string[] grants)
        {
            string result = grants[0];
            for (int n = 1; n < grants.Length; n++)
            {
                int maxlen = Math.Max(result.Length, grants[n].Length);
                byte[] bytes1 = ToBytes(Normalize(result, maxlen));
                byte[] bytes2 = ToBytes(Normalize(grants[n], maxlen));
                result = FromBytes(Combine(bytes1, bytes2));
            }
            return result;
        }

        /// <summary>
        /// Пересечение битовых наборов
        /// </summary>
        public static string Intersect(params string[] grants)
        {
            string result = grants[0];
            for (int n = 1; n < grants.Length; n++)
            {
                int maxlen = Math.Max(result.Length, grants[n].Length);
                byte[] bytes1 = ToBytes(Normalize(result, maxlen));
                byte[] bytes2 = ToBytes(Normalize(grants[n], maxlen));
                result = FromBytes(Intersect(bytes1, bytes2));
            }
            return result;
        }

        /// <summary>
        /// Вычитание двух битовых наборов
        /// </summary>
        public static string Subtract(string grant1, string grant2)
        {
            int maxlen = Math.Max(grant1.Length, grant2.Length);
            byte[] bytes1 = ToBytes(Normalize(grant1, maxlen));
            byte[] bytes2 = ToBytes(Normalize(grant2, maxlen));
            return FromBytes(Subtract(bytes1, bytes2));
        }

        /// <summary>
        /// Истинно, если в наборе есть хоть один ненулевой бит
        /// </summary>
        public static bool AnyFlag(string grants)
        {
            return AnyFlag(ToBytes(Normalize(grants, grants.Length)));
        }

        /// <summary>
        /// Проводит сравнение, есть ли хоть один общий бит в двух наборах
        /// </summary>
        public static bool AnyFlag(string grant1, string grant2)
        {
            int maxlen = Math.Max(grant1.Length, grant2.Length);
            byte[] bytes1 = ToBytes(Normalize(grant1, maxlen));
            byte[] bytes2 = ToBytes(Normalize(grant2, maxlen));
            return AnyFlag(Intersect(bytes1, bytes2));
        }

        public static bool AnyFlag(string grant1, params string[] grant2)
        {
            int maxlen;
            bool returned = false;
            maxlen = grant1.Length;

            foreach (string grant in grant2)
            {
                maxlen = Math.Max(maxlen, grant.Length);
            }
            byte[] bytes1 = ToBytes(Normalize(grant1, maxlen));
            foreach (string grant in grant2)
            {
                byte[] bytes2 = ToBytes(Normalize(grant, maxlen));
                if (returned = AnyFlag(Intersect(bytes1, bytes2)))
                    break;
            }
            return returned;
        }


        /// <summary>
        /// Возвращает список всех доступов, помеченных атрибутом GrantDetailsAttribute
        /// </summary>
        public static Grant[] AllGrants(Type type)
        {
            List<Grant> result = new List<Grant>();
            var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (var field in fields)
            {
                if (field.FieldType != typeof(string))
                    continue;

                GrantDetailsAttribute attr = field.GetCustomAttributes(typeof(GrantDetailsAttribute), false).FirstOrDefault() as GrantDetailsAttribute;
                if (attr == null)
                    continue;

                string value = (field.GetValue(null) as string) ?? "";
                Grant grant = new Grant(attr.Description, attr.Group, attr.OrderNum, value, attr.Permission);
                result.Add(grant);
            }

            return result.ToArray();
        }

        /// <summary>
        /// Проверяет доступ к методу контроллера в соответствии с набором доступов пользователя
        /// </summary>
        /// <param name="controllerType">
        /// Тип контроллера
        /// </param>
        /// <param name="action">
        /// Название метода
        /// </param>
        /// <param name="userGrants">
        /// Набор доступов пользователя (строка в виде шестнадцатеричного числа)
        /// </param>
        /// <param name="accessPath">
        /// Путь к защищенному ресурсу. Если он указан, попытка доступа пишется в лог
        /// </param>
        //public static bool AccessValidate(Type controllerType, ActionDescriptor action, string userGrants, string accessPath = null)
        //{
        //    var provider = Membership.Provider as NMProvider;

        //    if (action == null)
        //    {
        //        if (accessPath != null && provider.AccessLog)
        //        {
        //            var identity = HttpContext.Current.User.Identity;
        //            using (var dc = ProviderDataContext.Create(provider.ConnectionString))
        //            {
        //                AccessLog log = new AccessLog(string.IsNullOrEmpty(identity.Name) ? identity.Name : "anonymous",
        //                                              string.Format("Попытка доступа к несуществующему ресурсу: {0}", accessPath), false);
        //                dc.AccessLogs.InsertOnSubmit(log);
        //                dc.SubmitChanges();
        //            }
        //        }

        //        return false;
        //    }


        //    AllowGrantAttribute[] controllerAllow = (AllowGrantAttribute[])controllerType.GetCustomAttributes(typeof(AllowGrantAttribute), false);
        //    DenyGrantAttribute[] controllerDeny = (DenyGrantAttribute[])controllerType.GetCustomAttributes(typeof(DenyGrantAttribute), false);
        //    AllowGrantAttribute[] actionAllow = (AllowGrantAttribute[])action.GetCustomAttributes(typeof(AllowGrantAttribute), false);
        //    DenyGrantAttribute[] actionDeny = (DenyGrantAttribute[])action.GetCustomAttributes(typeof(DenyGrantAttribute), false);
        //    AllowWithoutGrantsAttribute[] withoutGrants = (AllowWithoutGrantsAttribute[])action.GetCustomAttributes(typeof(AllowWithoutGrantsAttribute), false);

        //    //  если нет никаких атрибутов - доступ свободный
        //    if (controllerAllow.Length == 0 && controllerDeny.Length == 0 && actionAllow.Length == 0 && actionDeny.Length == 0 && withoutGrants.Length == 0)
        //        return true;

        //    //  иначе нужна аутентификация
        //    if (!HttpContext.Current.Request.IsAuthenticated)
        //        return false;

        //    bool result = true;
        //    if (withoutGrants.Length == 0)
        //    {
        //        string grants = controllerAllow.Length > 0 ? controllerAllow[0].AllowGrants : "ALL";
        //        grants = controllerAllow.Aggregate(grants, (current, cAllow) => Combine(current, cAllow.AllowGrants));
        //        grants = controllerDeny.Aggregate(grants, (current, cDeny) => Subtract(current, cDeny.DenyGrants));

        //        if (actionAllow.Length > 0)
        //            grants = actionAllow[0].AllowGrants;
        //        grants = actionAllow.Aggregate(grants, (current, aAllow) => Combine(current, aAllow.AllowGrants));
        //        grants = actionDeny.Aggregate(grants, (current, aDeny) => Subtract(current, aDeny.DenyGrants));

        //        result = AnyFlag(Intersect(grants, userGrants));
        //    }

        //    if (accessPath != null && provider.AccessLog)
        //    {
        //        var identity = HttpContext.Current.User.Identity;
        //        using (var dc = ProviderDataContext.Create(provider.ConnectionString))
        //        {
        //            AccessLog log = new AccessLog(!string.IsNullOrEmpty(identity.Name) ? identity.Name : "anonymous",
        //                                          string.Format("Попытка доступа к ресурсу: {0}", accessPath), result);
        //            dc.AccessLogs.InsertOnSubmit(log);
        //            dc.SubmitChanges();
        //        }
        //    }

        //    return result;
        //}
    }

    public abstract class GrantAttribute : Attribute { }

    /// <summary>
    /// Описание - группа - порядковый номер в группе
    /// </summary>
    public class GrantDetailsAttribute : GrantAttribute
    {
        public readonly string Group = "";
        public readonly string Description = "";
        public readonly int OrderNum = 0;
        public PermissionEnum Permission;

        public GrantDetailsAttribute(string description, string group = "", int ordernum = 0, PermissionEnum perm = PermissionEnum.NULL)
        {
            Group = group;
            Description = description;
            OrderNum = ordernum;
            Permission = perm;
        }
    }

}
