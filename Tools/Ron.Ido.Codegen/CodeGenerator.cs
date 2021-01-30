using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Codegen
{
    internal class CodeGenerator
    {
        private List<string> EntryPoints = new List<string>();
        private Dictionary<Type, string> Models = new Dictionary<Type, string>();
        private string ApiName;
        private string NewLine = Environment.NewLine;

        public CodeGenerator(Type type)
        {
            ApiName = type.Name.Replace("Controller", "");
            var descriptors = MethodDescriptor.GetAll(type);

            foreach(var descriptor in descriptors)
            {
                switch (descriptor.HttpMethod)
                {
                    case "GET": CreateGetMethod(descriptor); break;
                    case "POST": CreatePostMethod(descriptor); break;
                }
            }
        }

        public string TypeScript
        {
            get
            {
                var builder = new StringBuilder();
                builder.AppendLine("//  Сгенерировано на основе серверного кода. Не изменять!!!");
                builder.AppendLine("import { WebApi } from '../../modules/webapi';");
                builder.AppendLine("");
                builder.AppendLine($"export namespace {ApiName}Api" + " {");
                builder.AppendLine(string.Join(NewLine, EntryPoints));
                builder.AppendLine(string.Join(NewLine, Models.Select(p => p.Value)));
                builder.AppendLine("}");

                return builder.ToString().Replace("\t", "    ");
            }
        }

        public string FileName
        {
            get
            {
                return CamelCase(ApiName) + "Api.ts";
            }
        }

        private void CreateGetMethod(MethodDescriptor descriptor)
        {
            var parInfos = descriptor.MethodInfo.GetParameters();
            var parNames = parInfos.Select(pi => pi.Name).ToList();
            var parDefs = new List<string>();
            foreach (var pi in parInfos)
            {
                parDefs.Add($"{pi.Name}:{GenerateType(pi.ParameterType)}");
            }

            var returnType = GenerateType(descriptor.MethodInfo.ReturnType);

            var segments = descriptor.Segments.Select(s => {
                if (s.StartsWith("{"))
                {
                    var segmPar = s.Trim('{', '}');
                    parNames.Remove(segmPar);
                    return segmPar;
                }

                return $"'{s}'";
            });

            var urlParams = parNames.Select(pn => "`" + pn + "=${" + pn + "}`");

            var builder = new StringBuilder();
            builder.AppendLine($"\texport function {CamelCase(descriptor.MethodName)}({string.Join(", ", parDefs)}): JQueryPromise<{returnType}>" + " {");
            builder.AppendLine($"\t\tconst segments = [{string.Join(", ", segments)}];");
            if (urlParams.Any())
            {
                builder.AppendLine($"\t\tconst urlParams = [{string.Join(", ", urlParams)}];");
                builder.AppendLine($"\t\treturn WebApi.get('/'+segments.join('/')+'?'+urlParams.join('&'));");
            }
            else
            {
                builder.AppendLine($"\t\treturn WebApi.get('/'+segments.join('/'));");
            }
            builder.AppendLine("\t}");

            EntryPoints.Add(builder.ToString());
        }

        private void CreatePostMethod(MethodDescriptor descriptor)
        {
            var parBodyInfo = descriptor.MethodInfo.GetParameters()
                .FirstOrDefault(pi => pi.GetCustomAttribute<FromBodyAttribute>() != null);

            var parBody = $"{parBodyInfo.Name}:{GenerateType(parBodyInfo.ParameterType)}";
            var returnType = GenerateType(descriptor.MethodInfo.ReturnType);
            var segments = descriptor.Segments.Select(s => $"'{s}'");

            var builder = new StringBuilder();
            builder.AppendLine($"\texport function {CamelCase(descriptor.MethodName)}({parBody}): JQueryPromise<{returnType}>" + " {");
            builder.AppendLine($"\t\tconst segments = [{string.Join(", ", segments)}];");
            builder.AppendLine($"\t\treturn WebApi.post('/'+segments.join('/'), {parBodyInfo.Name});");
            builder.AppendLine("\t}");

            EntryPoints.Add(builder.ToString());
        }

        private string GenerateType(Type type, string[] genericTypes = null)
        {
            if (NumberTypes.Contains(type))
            {

                return "number";
            }
            else if (StringTypes.Contains(type))
            {

                return "string";
            }
            else if (type.FullName == "System.Void")
            {
                return "any";
            }
            else if (type.IsArray)
            {
                var itemType = type.GetElementType();
                return $"{GenerateModel(itemType)}[]";
            }
            else if (type.IsGenericType)
            {
                var typeDef = type.GetGenericTypeDefinition();
                if (type.GetInterfaces().Any(ti => ti.Name.StartsWith("IEnumerable")))
                {
                    var itemType = type.GetGenericArguments().First();
                    if (genericTypes != null && genericTypes.Contains(itemType.Name))
                        return $"{itemType.Name}[]";

                    return $"{GenerateType(itemType)}[]";
                }
                else if (typeDef.FullName.StartsWith("System.Nullable") || typeDef.FullName.StartsWith("System.Threading.Task"))
                {
                    var itemType = type.GetGenericArguments().First();
                    return GenerateType(itemType);
                }

                var genName = GenerateGeneric(typeDef);
                var genPars = type.GetGenericArguments().Select(t => GenerateType(t));

                return $"{genName}<{string.Join(",", genPars)}>";
            }
            else if (type.IsClass)
            {
                if (type.FullName?.StartsWith("System.Threading.Task") ?? false)
                {
                    return "any";
                }

                if (genericTypes != null && genericTypes.Contains(type.Name))
                    return type.Name;

                return GenerateModel(type);
            }
            else if (type.IsEnum)
            {
                return GenerateEnum(type);
            }


            return "any";
        }

        private string GenerateModel(Type type)
        {
            var typeName = $"I{type.Name}";

            if (!Models.ContainsKey(type))
            {
                var builder = new StringBuilder();
                builder.AppendLine($"\t//  {type.FullName}");
                builder.AppendLine($"\texport interface {typeName}" + " {");
                foreach(var pi in type.GetProperties())
                {
                    builder.AppendLine($"\t\t{CamelCase(pi.Name)}:{GenerateType(pi.PropertyType)};");
                }
                builder.AppendLine("\t}");

                Models.Add(type, builder.ToString());
            }

            return typeName;
        }

        private string GenerateEnum(Type type)
        {
            if (!Models.ContainsKey(type))
            {
                var values = Enum.GetValues(type);
                var builder = new StringBuilder();
                builder.AppendLine($"\t//  {type.FullName}");
                builder.AppendLine($"\texport enum {type.Name}" + " {");
                var options = new List<string>();
                foreach (var item in values)
                {
                    options.Add($"\t\t{item} = {(int)item}");
                }
                builder.AppendLine(string.Join($",{Environment.NewLine}", options));
                builder.AppendLine("\t}");

                Models.Add(type, builder.ToString());
            }

            return type.Name;
        }

        private string GenerateGeneric(Type typeDef)
        {
            var typeName = $"I{typeDef.Name.Split("`").First()}";

            if (!Models.ContainsKey(typeDef))
            {
                var genPars = typeDef.GetGenericArguments().Select(t => t.Name).ToArray();

                var builder = new StringBuilder();
                builder.AppendLine($"\t//  {typeDef.FullName.Split("`").First()}<{string.Join(",", genPars)}>");
                builder.AppendLine($"\texport interface {typeName}<{string.Join(",", genPars)}>" + " {");
                foreach (var pi in typeDef.GetProperties())
                {
                    builder.AppendLine($"\t\t{CamelCase(pi.Name)}:{GenerateType(pi.PropertyType, genPars)};");
                }
                builder.AppendLine("\t}");

                Models.Add(typeDef, builder.ToString());
            }

            return typeName;
        }

        private string CamelCase(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            return text.Substring(0, 1).ToLower() + text.Substring(1);
        }

        private static bool IsPrimitive(Type type)
        {
            return type.IsPrimitive || type.Namespace == null || type.Namespace.Equals("System");

        }

        private static Type[] NumberTypes = new[] { typeof(Int16), typeof(Int32), typeof(Int64), typeof(UInt16), typeof(UInt32), typeof(UInt64), typeof(double) };
        private static Type[] StringTypes = new[] { typeof(DateTime), typeof(string) };
    }
}
