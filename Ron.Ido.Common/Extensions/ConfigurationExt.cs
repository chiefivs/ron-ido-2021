using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace Ron.Ido.Common.Extensions
{
    public static class ConfigurationExt
    {
        public static T GetSettings<T>(this IConfiguration conf) where T: class
        {
            var type = typeof(T);
            var attr = type.GetCustomAttribute<SectionNameAttribute>();
            var sectionName = attr?.Name ?? type.Name;

            var section = conf.GetSection(sectionName);
            return section.Get<T>();
        }
    }

    public class SectionNameAttribute: Attribute
    {
        public string Name { get; private set; }

        public SectionNameAttribute(string name)
        {
            Name = name;
        }
    }
}
