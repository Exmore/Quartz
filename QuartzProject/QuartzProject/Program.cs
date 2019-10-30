using QuartzProject.Actions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace QuartzProject
{
    class Program
    {

        private static Dictionary<string, IAction> actionsDictionary;

        private static void FillActionsDictionary()
        {
            actionsDictionary = new Dictionary<string, IAction>();

            var deleteAction = new DeleteAction();
            var pauseAction = new PauseAction();
            var reInitAction = new ReInitAction();
            var resumeAction = new ResumeAction();
            var error1 = new InitErrorJobAction();
            var error2 = new InitErrorJobAction2();

            actionsDictionary.Add(deleteAction.Title.ToLower(), deleteAction);
            actionsDictionary.Add(pauseAction.Title.ToLower(), pauseAction);
            actionsDictionary.Add(reInitAction.Title.ToLower(), reInitAction);
            actionsDictionary.Add(resumeAction.Title.ToLower(), resumeAction);
            actionsDictionary.Add(error1.Title.ToLower(), error1);
            actionsDictionary.Add(error2.Title.ToLower(), error2);
        }

        private static string GetAllAction()
        {
            return string.Join(" ", actionsDictionary.Keys);
        }

        static void Main(string[] args)
        {
            FillActionsDictionary();

            // В случае разных имен инстанса они будут жить независимо друг от друга.
            // Если 1 имя, то из 1 инстанса можно влиять на джобы другого.

            Console.WriteLine("Instance name?");
            var instanceName = Console.ReadLine();

            instanceName = string.IsNullOrEmpty(instanceName) ? "instance_one" : instanceName;

            var currentScheduler = StartUp.StartUp.ConfigurateScheduler(instanceName);

            Console.WriteLine($"Avaible actions: {GetAllAction()}\n");
            var action = Console.ReadLine();

            if (actionsDictionary.ContainsKey(action.ToLower()))
            {
                var props = "";

                if(actionsDictionary[action].ArePropsNeeded)
                {
                    props = "Vasyan1";
                }

                actionsDictionary[action].Execute(currentScheduler, props);
            }

            Console.WriteLine("Start the scheduler? y = yes\n");
            var res = Console.ReadLine();

            if(res.Equals("y"))
            {
                currentScheduler.Start();

                var i = 0;
                while (true)
                {
                    i++;
                    Console.WriteLine(i);
                    Thread.Sleep(1000);

                    var count = currentScheduler.GetCurrentlyExecutingJobs().Result;
                    Console.WriteLine($"Currently Executing Jobs Count = {count?.Count}");
                }
            }            
        }
    }
}
