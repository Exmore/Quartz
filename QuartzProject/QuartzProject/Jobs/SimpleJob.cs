using Quartz;
using QuartzProject.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzProject.Jobs
{
    // Чтобы был 1 лишь инстанс джобы в рамках конкретного шедулера
    [DisallowConcurrentExecution]
    class SimpleJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Thread.Sleep(5000);
            JobDataMap dataMap = context.MergedJobDataMap;

            string triggerparam = dataMap.GetString("triggerparam");
            var user = (SimpleJobParameter)dataMap.Get("user");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Hello from job. UserName = {user?.UserName}, Password = {user?.Password}. TriggerParama = {triggerparam}. Time = {DateTime.Now}");
            Console.ResetColor();            
        }
    }
}
