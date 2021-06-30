using Microsoft.AspNetCore.Mvc;
using Ron.Ido.Common.Attributes;
using Ron.Ido.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Codegen
{
    internal class CodeGenerator
    {
        private Dictionary<string, CodeModule> Modules;
        private List<string> EntryPoints = new List<string>();
        private string ApiName;
        private string ApiModuleName { get { return $"{ApiName.ToCamel()}Api"; } }
        private string NewLine = Environment.NewLine;

        public CodeGenerator(Type type, Dictionary<string,  CodeModule> modules)
        {
            Modules = modules;
            ApiName = type.Name.Replace("Controller", "");
            var descriptors = MethodDescriptor.GetAll(type);

            foreach(var descriptor in descriptors)
            {
                switch (descriptor.HttpMethod)
                {
                    case "GET": CreateGetMethod(descriptor); break;
                    case "DELETE": CreateDelMethod(descriptor); break;
                    case "POST": CreatePostMethod(descriptor); break;
                }
            }
        }

        public string FileName { get { return ApiModuleName + ".ts"; } }

        public string TypeScript
        {
            get
            {
                var builder = new StringBuilder();
                builder.AppendLine("//  Сгенерировано на основе серверного кода. Не изменять!!!");
                builder.AppendLine("import { WebApi } from '../../modules/webapi';");

                var module = Modules.ContainsKey(ApiModuleName) ? Modules[ApiModuleName] : null;

                if(module != null)
                {
                    foreach (var import in module.Imports)
                    {
                        builder.AppendLine($"import {{ {import.Value.Join(", ")} }} from './{import.Key}';");
                    }
                }

                builder.AppendLine("");
                builder.AppendLine($"export namespace {ApiName}Api" + " {");
                builder.AppendLine(string.Join(NewLine, EntryPoints));

                if (module != null)
                {
                    builder.AppendLine(string.Join(NewLine, module.Models.Select(p => p.Value)));
                }

                builder.AppendLine("}");

                return builder.ToString().Replace("\t", "    ");
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
            builder.AppendLine($"\texport function {descriptor.MethodName.ToCamel()}({string.Join(", ", parDefs)}): JQueryPromise<{returnType}>" + " {");
            builder.AppendLine($"\t\tconst segments = [{string.Join(", ", segments)}];");
            if (urlParams.Any())
            {
                builder.AppendLine($"\t\tconst urlParams = [{string.Join(", ", urlParams)}];");
                builder.AppendLine($"\t\treturn WebApi.get(segments.join('/')+'?'+urlParams.join('&'));");
            }
            else
            {
                builder.AppendLine($"\t\treturn WebApi.get(segments.join('/'));");
            }
            builder.AppendLine("\t}");

            EntryPoints.Add(builder.ToString());
        }

        private void CreateDelMethod(MethodDescriptor descriptor)
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
            builder.AppendLine($"\texport function {descriptor.MethodName.ToCamel()}({string.Join(", ", parDefs)}): JQueryPromise<{returnType}>" + " {");
            builder.AppendLine($"\t\tconst segments = [{string.Join(", ", segments)}];");
            if (urlParams.Any())
            {
                builder.AppendLine($"\t\tconst urlParams = [{string.Join(", ", urlParams)}];");
                builder.AppendLine($"\t\treturn WebApi.get(segments.join('/')+'?'+urlParams.join('&'));");
            }
            else
            {
                builder.AppendLine($"\t\treturn WebApi.del(segments.join('/'));");
            }
            builder.AppendLine("\t}");

            EntryPoints.Add(builder.ToString());
        }

        private void CreatePostMethod(MethodDescriptor descriptor)
        {
            var parBodyInfo = descriptor.MethodInfo.GetParameters()
                .FirstOrDefault(pi => pi.GetCustomAttribute<FromBodyAttribute>() != null);

            var parBody = parBodyInfo != null ? $"{parBodyInfo.Name}:{GenerateType(parBodyInfo.ParameterType)}" : "";
            var returnType = GenerateType(descriptor.MethodInfo.ReturnType);
            var segments = descriptor.Segments.Select(s => $"'{s}'");

            var builder = new StringBuilder();
            builder.AppendLine($"\texport function {descriptor.MethodName.ToCamel()}({parBody}): JQueryPromise<{returnType}>" + " {");
            builder.AppendLine($"\t\tconst segments = [{string.Join(", ", segments)}];");
            builder.AppendLine(parBodyInfo != null
                ? $"\t\treturn WebApi.post(segments.join('/'), {parBodyInfo.Name});"
                : $"\t\treturn WebApi.post(segments.join('/'));");
            builder.AppendLine("\t}");

            EntryPoints.Add(builder.ToString());
        }

        private string GenerateType(Type type)
        {
            if (!Modules.ContainsKey(ApiModuleName))
            {
                Modules.Add(ApiModuleName, new CodeModule(ApiModuleName, Modules));
            }

            return Modules[ApiModuleName].GenerateType(type);
        }
    }

    internal class CodeModule
    {
        public string ModuleName;
        public Dictionary<string, List<string>> Imports = new Dictionary<string, List<string>>();
        public Dictionary<Type, string> Models = new Dictionary<Type, string>();
        
        private string NewLine = Environment.NewLine;
        private Dictionary<string, CodeModule> Modules;
        private static Type BooleanType = typeof(bool);
        private static Type[] NumberTypes = new[] { typeof(Int16), typeof(Int32), typeof(Int64), typeof(UInt16), typeof(UInt32), typeof(UInt64), typeof(double) };
        private static Type[] StringTypes = new[] { typeof(DateTime), typeof(string) };

        public CodeModule(string name, Dictionary<string, CodeModule> modules)
        {
            ModuleName = name;
            Modules = modules;
        }

        public string TypeScript
        {
            get
            {
                var builder = new StringBuilder();
                builder.AppendLine("//  Сгенерировано на основе серверного кода. Не изменять!!!");
                foreach (var import in Imports)
                {
                    builder.AppendLine($"import {{ {import.Value.Join(", ")} }} from './{import.Key}';");
                }

                builder.AppendLine(string.Join(NewLine, Models.Select(p => p.Value)));
                return builder.ToString().Replace("\t", "    ");
            }
        }

        public string GenerateType(Type type, string[] genericTypes = null)
        {
            if (NumberTypes.Contains(type))
            {
                return "number";
            }
            else if (StringTypes.Contains(type))
            {
                return "string";
            }
            else if(type == BooleanType)
            {
                return "boolean";
            }
            else if (type.FullName == "System.Void")
            {
                return "any";
            }
            else if (type.IsArray)
            {
                var itemType = type.GetElementType();
                return $"{GenerateType(itemType)}[]";
            }
            else if (type.IsGenericType)
            {
                var typeDef = type.GetGenericTypeDefinition();

                if (typeDef.Name.StartsWith("Dictionary"))
                {
                    var argTypes = type.GetGenericArguments();
                    var keyType = argTypes[0];
                    var valType = argTypes[1];

                    return $"{{[key:{GenerateType(keyType)}]:{GenerateType(valType)}}}";
                }
                else if (type.GetInterfaces().Any(ti => ti.Name.StartsWith("IEnumerable")))
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

                if (type.FullName == "System.Object")
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
            var module = GetModule(type, ModuleName);

            if (!module.Models.ContainsKey(type))
            {
                module.Models.Add(type, "");
                var builder = new StringBuilder();
                builder.AppendLine($"\t//  {type.FullName}");
                builder.AppendLine($"\texport interface {typeName}" + " {");

                var properties = type.GetProperties();
                foreach (var pi in properties)
                {
                    builder.AppendLine($"\t\t{pi.Name.ToCamel()}:{module.GenerateType(pi.PropertyType)};");
                }
                builder.AppendLine("\t}");

                module.Models[type] = builder.ToString();
            }

            AddImports(typeName, module);
            return typeName;
        }

        private string GenerateEnum(Type type)
        {
            var module = GetModule(type, ModuleName);

            if (!module.Models.ContainsKey(type))
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

                module.Models.Add(type, builder.ToString());
            }

            AddImports(type.Name, module);
            return type.Name;
        }

        private string GenerateGeneric(Type type)
        {
            var typeName = $"I{type.Name.Split("`").First()}";
            var module = GetModule(type, ModuleName);

            if (!module.Models.ContainsKey(type))
            {
                module.Models.Add(type, "");
                var genPars = type.GetGenericArguments().Select(t => t.Name).ToArray();

                var builder = new StringBuilder();
                builder.AppendLine($"\t//  {type.FullName.Split("`").First()}<{string.Join(",", genPars)}>");
                builder.AppendLine($"\texport interface {typeName}<{string.Join(",", genPars)}>" + " {");
                foreach (var pi in type.GetProperties())
                {
                    builder.AppendLine($"\t\t{pi.Name.ToCamel()}:{module.GenerateType(pi.PropertyType, genPars)};");
                }
                builder.AppendLine("\t}");

                module.Models[type] = builder.ToString();
            }

            AddImports(typeName, module);
            return typeName;
        }

        private static bool IsPrimitive(Type type)
        {
            return type.IsPrimitive || type.Namespace == null || type.Namespace.Equals("System");

        }

        private CodeModule GetModule(Type type, string defModuleName)
        {
            var moduleAttr = type.GetCustomAttribute<TypeScriptModuleAttribute>();
            var moduleName = moduleAttr?.Name ?? defModuleName;

            if (!Modules.ContainsKey(moduleName))
                Modules[moduleName] = new CodeModule(moduleName, Modules);

            var module = Modules[moduleName];
            //var models = module.Models;

            //if (!Imports.ContainsKey(moduleName))
            //    Imports.Add(moduleName, new List<string>());

            return module;
        }

        private void AddImports(string typeName, CodeModule module)
        {
            if (module != this)
            {
                if (!Imports.ContainsKey(module.ModuleName))
                {
                    Imports.Add(module.ModuleName, new List<string>());
                }

                if (!Imports[module.ModuleName].Contains(typeName))
                {
                    Imports[module.ModuleName].Add(typeName);
                }
            }
        }
    }
}
