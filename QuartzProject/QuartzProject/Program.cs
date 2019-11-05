using Quartz;
using QuartzProject.Actions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzProject
{
    class Program
    {
        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            var fileName = "newTxtFile.txt";
            var dirPath = Directory.GetCurrentDirectory() + @"\\" + fileName;

            File.WriteAllText(dirPath, $"Exit in {DateTime.Now}");
            var currentScheduler = StartUp.StartUp.ConfigurateScheduler(instName);
            currentScheduler.Shutdown(true);
            File.AppendAllText(dirPath, "\nExit was done");
        }

        private static Dictionary<string, IAction> actionsDictionary;
        private static string instName;

        private static void FillActionsDictionary()
        {
            var interfaceType = typeof(IAction);

            actionsDictionary = AppDomain.CurrentDomain.GetAssemblies()
              .SelectMany(x => x.GetTypes())
              .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
              .Select(x => Activator.CreateInstance(x))
              .ToDictionary(
                  key =>
                  {
                      var currentValue = key as IAction;
                      return currentValue.Title.ToLower();
                  },
                  value =>
                  {
                      var currentValue = value as IAction;
                      return currentValue;
                  });
        }

        private static string GetAllAction()
        {
            return string.Join("\n", actionsDictionary.Keys);
        }

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            FillActionsDictionary();

            // В случае разных имен инстанса они будут жить независимо друг от друга.
            // Если 1 имя, то из 1 инстанса можно влиять на джобы другого.

            Console.WriteLine("Instance name?");
            var instanceName = Console.ReadLine();

            instanceName = string.IsNullOrEmpty(instanceName) ? "instance_one" : instanceName;
            instName = instanceName;

            var currentScheduler = StartUp.StartUp.ConfigurateScheduler(instanceName);                                    

            Console.WriteLine($"Доступные действия: \n{GetAllAction()}\n");
            var action = Console.ReadLine();

            if (actionsDictionary.ContainsKey(action.ToLower()))
            {                
                actionsDictionary[action].Execute(currentScheduler);
            }

            Console.WriteLine("Запустить шедулер? y = yes\n");
            var res = Console.ReadLine();

            if (res.Equals("y"))
            {
                currentScheduler.Start();
                DoInfinityLoop(currentScheduler);                
            }

            Console.WriteLine("Нажмите любую кнопку для завершения работы (программа завершиться, после отработки всех джобов)");
            Console.ReadLine();
            currentScheduler.Shutdown(true);
        }

        private static async void DoInfinityLoop(IScheduler currentScheduler)
        {
            var loop = new TaskFactory().StartNew(() =>
            {
                var i = 0;
                while (true)
                {
                    i++;
                    Console.WriteLine(i);
                    Thread.Sleep(1000);

                    var count = currentScheduler.GetCurrentlyExecutingJobs().Result;
                    Console.WriteLine($"Currently Executing Jobs Count = {count?.Count}");
                }
            });
            
            await loop;
        }
    }
}
