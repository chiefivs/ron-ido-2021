using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    case "GET":
                        var parInfos = descriptor.MethodInfo.GetParameters();
                        var parNames = parInfos.Select(pi => pi.Name).ToList();
                        var parDefs = new List<string>();
                        foreach(var pi in parInfos)
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
                        break;
                }
            }
        }

        public string TypeScript
        {
            get
            {
                var builder = new StringBuilder();
                builder.AppendLine("import WebApi from '../../modules/webapi';");
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

        private string GenerateType(Type type)
        {
            if (NumberTypes.Contains(type))
            {

                return "number";
            }
            else if (StringTypes.Contains(type))
            {

                return "string";
            }
            else if (type.IsArray)
            {
                var itemType = type.GetElementType();
                return $"{GenerateModel(itemType)}[]";
            }
            else if (type.IsGenericType)
            {
                if (type.GetGenericTypeDefinition().Name.StartsWith("IEnumerable"))
                {
                    var itemType = type.GetGenericArguments().First();
                    return $"{GenerateModel(itemType)}[]";
                }

                return "any";
            }
            else if (type.IsClass)
            {

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
                    builder.AppendLine($"\t\t{CamelCase(pi.Name)}:{GenerateType(pi.PropertyType)}");
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

        private string CamelCase(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            return text.Substring(0, 1).ToLower() + text.Substring(1);
        }

        private static Type[] NumberTypes = new[] { typeof(Int16), typeof(Int32), typeof(Int64), typeof(UInt16), typeof(UInt32), typeof(UInt64), typeof(double) };
        private static Type[] StringTypes = new[] { typeof(DateTime), typeof(string) };
    }
}
