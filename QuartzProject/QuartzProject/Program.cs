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
            Console.WriteLine("Start? y\n");
            var res = Console.ReadLine();

            if(res.Equals("y"))
            {
                var currentScheduler = ConfigurateScheduler();
                InitAndStartJob(currentScheduler);                
            }

            var i = 0;
            while (true)
            {
                i++;
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
        }

        //private static async void InitAndStartJob(IScheduler scheduler)
        //{
        //    IJobDetail job = JobBuilder.Create<SimpleJob>()
        //        .WithIdentity("simpleJob", "someGroup")
        //        .Build();
        //    job.JobDataMap.Put("user", new SimpleJobParameter { UserName = "Vasya", Password = "123" });

        //    ITrigger trigger = TriggerBuilder.Create()
        //        .UsingJobData("triggerparam", "Some trigger param")
        //        .WithIdentity("someTrigger", "triggerGroup")
        //        .StartNow()
        //        //1ый способ
        //        .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(1)).RepeatForever())
        //        //2й способ
        //        //.WithDailyTimeIntervalSchedule(
        //        //    x => x.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
        //        //    .OnDaysOfTheWeek(DayOfWeek.Monday, DayOfWeek.Wednesday)
        //        //    .EndingDailyAfterCount(22)
        //        //    .WithRepeatCount(5))
        //        //3й способ
        //        //.WithCronSchedule("0 0/1 * 1/1 * ? *")
        //        .Build();

        //    await scheduler.ScheduleJob(job, trigger);
        //}

        private static async void InitAndStartJob(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<SimpleJob>()
                .WithIdentity("simpleJob", "someGroup")
                .Build();
            job.JobDataMap.Put("user", new SimpleJobParameter { UserName = "Vasya", Password = "123" });

            ITrigger trigger = TriggerBuilder.Create()
                .UsingJobData("triggerparam", "Some trigger param")
                .WithIdentity("someTrigger", "triggerGroup")
                .StartNow()
                //1ый способ
                .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(2)).RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        //private static IScheduler ConfigurateScheduler()
        //{
        //    NameValueCollection props = new NameValueCollection
        //    {
        //        {"quartz.serializer.type","binary" },
        //        //{"quartz.jobStore.type", "Quartz.Simpl.RAMJobStore, Quartz" }
        //        //{"quartz.jobStore.driverDelegateType","Quartz.Impl.AdoJobStore.OracleDelegate" }
        //    };

        //    StdSchedulerFactory factory = new StdSchedulerFactory(props);
        //    var scheduler = factory.GetScheduler().Result;

        //    scheduler.Start().Wait();
        //    //scheduler.ListenerManager.AddTriggerListener(new TriggerListener()/*, GroupMatcher<TriggerKey>.GroupEquals("triggerexample")*/);
        //    scheduler.ListenerManager.AddJobListener(new JobListener());
        //    scheduler.ListenerManager.AddSchedulerListener(new SchedulerListener());
        //    return scheduler;
        //}

        private static IScheduler ConfigurateScheduler()
        {
            //using (var connection = new OracleConnection("Data Source=localhost:1521/xe; User Id=NewUser;Password=123456;"))
            //{
            //    connection.Open();
            //}

            NameValueCollection props = new NameValueCollection();
            //props["org.quartz.dataSource.myDS.driver"] = "oracle.jdbc.pool.OracleDataSource";
            props["quartz.serializer.type"] = "binary";
            props["quartz.scheduler.instanceName"] = "TestScheduler";
            props["quartz.scheduler.instanceId"] = "instance_one";
            props["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            props["quartz.jobStore.useProperties"] = "true";
            props["quartz.jobStore.dataSource"] = "default";
            props["quartz.jobStore.tablePrefix"] = "QRTZ_";
            props["quartz.dataSource.default.connectionString"] = "Data Source=localhost:1521/xe;User Id=NewUser;Password=123456;";
            props["quartz.dataSource.default.provider"] = "OracleODP";
            props["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.OracleDelegate, Quartz";

            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            var scheduler = factory.GetScheduler().Result;

            scheduler.Start().Wait();
            return scheduler;
        }
    }
}
