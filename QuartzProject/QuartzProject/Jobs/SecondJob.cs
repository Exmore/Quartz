using Quartz;
using QuartzProject.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzProject.Jobs
{    
    public class SecondJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Thread.Sleep(1000);
            JobDataMap dataMap = context.MergedJobDataMap;

            string triggerparam = dataMap.GetString("triggerparam");
            var user = (SecondJobParameter)dataMap.Get("user");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Hello from job. UserName = {user?.UserName}, Password = {user?.Password}. TriggerParama = {triggerparam}");
            Console.ResetColor();
        }
    }
}
