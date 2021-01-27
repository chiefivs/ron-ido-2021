using Microsoft.AspNetCore.Mvc;
using System;
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

            foreach(var controller in controllers)
            {
                var code = new CodeGenerator(controller);
                var script = code.TypeScript;

                var webapiDir = @"ClientApp\codegen\webapi";
                if (Directory.Exists(webapiDir))
                {
                    var filePath = Path.Combine(webapiDir, code.FileName);
                    File.WriteAllText(filePath, script);
                    Console.WriteLine($"{filePath} создан");
                }
                else
                {
                    Console.WriteLine(script);
                }

            }
        }
    }
}
