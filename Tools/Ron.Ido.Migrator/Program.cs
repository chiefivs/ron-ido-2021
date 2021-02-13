using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ron.Ido.EM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ron.Ido.Migrator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Признание ИДО - мигратор базы данных");
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

			using (var context = serviceProvider.GetService<AppDbContext>())
			{
				Console.WriteLine($"Тип контекста - {context.GetType().Name}");
				Console.WriteLine("Миграция структуры...");

				context.Database.Migrate();
				Console.WriteLine("Миграция успешно завершена");

				if (args.Contains("no-data"))
				{
					Console.ReadKey();
					return;
				}

				var taskList = typeof(IUpdateTask).Assembly.GetTypes()
					.Where(t => t.IsClass && typeof(IUpdateTask).IsAssignableFrom(t))
					.Select(Activator.CreateInstance)
					.Cast<IUpdateTask>();

				var tasks = new Queue<IUpdateTask>(taskList);
				var hash = taskList.Select(iut => iut.GetType()).ToHashSet();

				Console.WriteLine("Модификация данных...");
				int idleSteps = 0;
				while (tasks.Count > 0)
				{
					var task = tasks.Dequeue();
					Console.Write($"Задача {task.GetType().Name} ");

					try
					{
						if (idleSteps > tasks.Count + 1)
						{
							Console.WriteLine("Предположительно циклическая зависимость в очереди");
							break;
						}

						context.BeginTransaction();
						task.Update(context);
						context.SaveChanges();
						context.Commit();
						Console.WriteLine($" успешно выполнена");
						idleSteps = 0;
					}
					catch (Exception ex)
					{
						context.Rollback();
						Console.WriteLine($" выполнить не удалось{Environment.NewLine}{ex.Message} {ex.InnerException}");
					}
					hash.Remove(task.GetType());
				}

				Console.WriteLine("Модификация завершена, для завершения нажмите любую клавишу");

				Console.ReadKey();
			}
		}
	}
}
