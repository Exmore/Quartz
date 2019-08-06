using Quartz;
using QuartzProject.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzProject.Jobs
{
    class SimpleJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.MergedJobDataMap;

            string triggerparam = dataMap.GetString("triggerparam");
            var user = (SimpleJobParameter)dataMap.Get("user");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Hello from job. UserName = {user.UserName}, Password = {user.Password}. TriggerParama = {triggerparam}");
            Console.ResetColor();

            Thread.Sleep(2000);
        }
    }
}
