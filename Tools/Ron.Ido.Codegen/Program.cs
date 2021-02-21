using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Codegen
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.GetAssembly(typeof(RonFde.WebInternal.Program));
            var controllers = assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<ApiControllerAttribute>() != null);

            var webapiDir = @"ClientApp\codegen\webapi";
            var modules = new Dictionary<string, CodeModule>();
            foreach(var controller in controllers)
            {
                var code = new CodeGenerator(controller, modules);
                var script = code.TypeScript;

                if (Directory.Exists(webapiDir))
                {
                    var filePath = Path.Combine(webapiDir, code.FileName);
                    File.WriteAllText(filePath, script);
                    Console.WriteLine($"{filePath} создан");
                }
                else
                {
                    Console.WriteLine($"//  файл '{code.FileName}'");
                    Console.WriteLine(script);
                }
            }

            foreach(var module in modules.Where(m => !m.Value.ModuleName.EndsWith("Api")))
            {
                var script = module.Value.TypeScript;
                var moduleName = $"{module.Key}.ts";

                if (Directory.Exists(webapiDir))
                {
                    var filePath = Path.Combine(webapiDir, moduleName);
                    File.WriteAllText(filePath, script);
                    Console.WriteLine($"{filePath} создан");
                }
                else
                {
                    Console.WriteLine($"//  файл '{moduleName}'");
                    Console.WriteLine(script);
                }
            }
        }
    }
}
