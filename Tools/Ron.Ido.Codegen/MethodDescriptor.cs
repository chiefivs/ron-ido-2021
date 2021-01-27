using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Codegen
{
    internal class MethodDescriptor
    {
        public string[] Segments;
        public string HttpMethod;
        public MethodInfo MethodInfo;
        public string MethodName => MethodInfo.Name;

        private static readonly Type routeAttrType;

        public MethodDescriptor(Type type, string name, object[] args)
            : this(GetInterfaceSegments(type), type, GetMethodInfo(type, name, args))
        {
        }

        static MethodDescriptor()
        {
            routeAttrType = typeof(RouteAttribute);
        }

        private MethodDescriptor(string[] interfaceSegments, Type type, MethodInfo methodInfo)
        {
            var attributes = methodInfo.GetCustomAttributes(false);
            HttpMethod = GetHttpMethodFromAttributes(attributes) ?? GetHttpMethodFromName(methodInfo);

            var routeAttr = attributes.FirstOrDefault(a => a.GetType() == routeAttrType) as RouteAttribute;

            var segments = new List<string>();

            if (interfaceSegments != null)
                segments.AddRange(interfaceSegments);

            if (routeAttr != null)
                segments.AddRange(GetRouteSegments(routeAttr, type));
            else
                segments.Add(methodInfo.Name.ToLower());

            Segments = segments.ToArray();
            MethodInfo = methodInfo;
        }

        public static IEnumerable<MethodDescriptor> GetAll(Type type)
        {
            var rootSegments = GetInterfaceSegments(type);
            var descriptors = type.GetMethods()
                .Where(mtdInfo => mtdInfo.DeclaringType == type && mtdInfo.IsPublic)
                .Select(mtdInfo => new MethodDescriptor(rootSegments, type, mtdInfo));

            return descriptors;
        }

        public override string ToString()
        {
            return $"{HttpMethod} {string.Join("/", Segments)}";
        }

        private static string[] GetRouteSegments(RouteAttribute routeAttr, Type type)
        {
            var controllerName = type.Name;
            if (controllerName.EndsWith("Controller"))
                controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);

            var template = (routeAttr?.Template ?? "").Replace("[controller]", controllerName);
            return template.Trim('/').Split('/').Where(s => s != string.Empty).ToArray();
        }

        private static string[] GetInterfaceSegments(Type type)
        {
            var routeAttr = type.GetCustomAttribute<RouteAttribute>();
            return GetRouteSegments(routeAttr, type);
        }

        private static MethodInfo GetMethodInfo(Type type, string name, object[] args)
        {
            return type.GetMethods().First(m => {
                if (m.Name != name)
                    return false;

                var pars = m.GetParameters();
                if (pars.Length != args.Length)
                    return false;

                for (int n = 0; n < pars.Length; n++)
                {
                    if (args[n] != null && pars[n].ParameterType != args[n].GetType())
                        return false;
                }

                return true;
            });
        }

        private static string GetHttpMethodFromAttributes(object[] attributes)
        {
            if (attributes.Any(a => a.GetType() == typeof(HttpDeleteAttribute)))
                return HttpMethods.Delete;

            if (attributes.Any(a => a.GetType() == typeof(HttpGetAttribute)))
                return HttpMethods.Get;

            if (attributes.Any(a => a.GetType() == typeof(HttpPatchAttribute)))
                return HttpMethods.Patch;

            if (attributes.Any(a => a.GetType() == typeof(HttpPostAttribute)))
                return HttpMethods.Post;

            if (attributes.Any(a => a.GetType() == typeof(HttpPutAttribute)))
                return HttpMethods.Put;

            return null;
        }

        private static string GetHttpMethodFromName(MethodInfo mtd)
        {
            var name = mtd.Name.ToUpper();

            if (name.StartsWith(HttpMethods.Delete))
                return HttpMethods.Delete;

            if (name.StartsWith(HttpMethods.Get))
                return HttpMethods.Get;

            if (name.StartsWith(HttpMethods.Patch))
                return HttpMethods.Patch;

            if (name.StartsWith(HttpMethods.Post))
                return HttpMethods.Post;

            if (name.StartsWith(HttpMethods.Put))
                return HttpMethods.Put;

            return HttpMethods.Get;
        }
    }
}
