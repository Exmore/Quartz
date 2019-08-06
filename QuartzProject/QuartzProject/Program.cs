using Oracle.ManagedDataAccess.Client;
using Quartz;
using Quartz.Impl;
using QuartzProject.Jobs;
using QuartzProject.Models;
using System;
using System.Collections.Specialized;
using System.Threading;

namespace QuartzProject
{
    class Program
    {        
        static void Main(string[] args)
        {            
            
            Console.WriteLine("Re-Init? y\n");
            var res = Console.ReadLine();

            var currentScheduler = ConfigurateScheduler();

            if (res.Equals("y"))
            {                
                InitAndStartJob(currentScheduler);                
            }

            currentScheduler.Start();

            var i = 0;
            while (true)
            {
                i++;
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
        }

        private static async void InitAndStartJob(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<SimpleJob>()
                .WithIdentity("simpleJob", "someGroup")
                .Build();
            job.JobDataMap.Put("user", new SimpleJobParameter { UserName = "Vasya", Password = "123" });
            
            if (await scheduler.CheckExists(job.Key))
            {
                Console.WriteLine("Job already exists!");

                //Если захотим по-новой создать, нужно сначал удалить, иначе будет краш бд
                //scheduler.DeleteJob(job.Key);
            }
            else
            {
                Console.WriteLine("It's ok. You can schedule this job.");

                ITrigger trigger = TriggerBuilder.Create()
                    .UsingJobData("triggerparam", "Some trigger param")
                    .WithIdentity("someTrigger", "triggerGroup")
                    .StartNow()
                    //1ый способ
                    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(2)).RepeatForever())
                    //2й способ
                    //.WithDailyTimeIntervalSchedule(
                    //    x => x.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                    //    .OnDaysOfTheWeek(DayOfWeek.Monday, DayOfWeek.Wednesday)
                    //    .EndingDailyAfterCount(22)
                    //    .WithRepeatCount(5))
                    //3й способ
                    //.WithCronSchedule("0 0/1 * 1/1 * ? *")
                    .Build();                    

                await scheduler.ScheduleJob(job, trigger);
            }
        }

        private static IScheduler ConfigurateScheduler()
        {
            NameValueCollection props = new NameValueCollection();

            props["quartz.serializer.type"] = "json";
            props["quartz.scheduler.instanceName"] = "TestScheduler";
            //props["quartz.scheduler.instanceId"] = "instance_one";
            props["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            // Нужно понять как поставить true
            props["quartz.jobStore.useProperties"] = "false";
            props["quartz.jobStore.dataSource"] = "default";
            props["quartz.jobStore.tablePrefix"] = "QRTZ_";
            props["quartz.dataSource.default.connectionString"] = "Data Source=localhost:1521/xe;User Id=NewUser;Password=123456;";
            props["quartz.dataSource.default.provider"] = "OracleODP";
            props["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.OracleDelegate, Quartz";

            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            var scheduler = factory.GetScheduler().Result;
            
            return scheduler;
        }
    }
}
