using System;
using Quartz;
using QuartzProject.Jobs;
using QuartzProject.Models;

namespace QuartzProject.Actions
{
    public class ReInitAction : IAction
    {
        public async void Execute(IScheduler scheduler)
        {
            var props = GetPropsMethod();

            var name = props.Length > 0 ? props[0].ToString() : "Vasyan";
            var password = props.Length > 1 ? props[1].ToString() : "VasyanPasswords";

            Console.WriteLine("1 2 3 4 5? (1 раз в 5 секунд, остальные каждую секунду)");

            IJobDetail job;
            var triggerName = "";

            var value = Console.ReadLine();

            if (value.Equals("1"))
            {
                job = JobBuilder.Create<SimpleJob>()
                    .WithIdentity("simpleJob", "someGroup")                    
                    .Build();                

                job.JobDataMap.Put("user", new SimpleJobParameter { UserName = name, Password = password });

                if (await scheduler.CheckExists(job.Key))
                {
                    Console.WriteLine("Job already exists!");
                    await scheduler.DeleteJob(job.Key);
                }

                triggerName = "simpleJobTrigger";
            }
            else if (value.Equals("2"))
            {
                job = JobBuilder.Create<SecondJob>()
                    .WithIdentity("secondJob", "someGroup")
                    .Build();

                job.JobDataMap.Put("user", new SecondJobParameter { UserName = name, Password = password });

                if (await scheduler.CheckExists(job.Key))
                {
                    Console.WriteLine("Job already exists!");
                    await scheduler.DeleteJob(job.Key);
                }

                triggerName = "secondJobTrigger";
            }
            else if (value.Equals("3"))
            {
                job = JobBuilder.Create<SecondJob2>()
                    .WithIdentity("secondJob2", "someGroup")
                    .Build();

                job.JobDataMap.Put("user", new SecondJobParameter { UserName = name, Password = password });

                if (await scheduler.CheckExists(job.Key))
                {
                    Console.WriteLine("Job already exists!");
                    await scheduler.DeleteJob(job.Key);
                }

                triggerName = "secondJobTrigger2";
            }
            else if (value.Equals("4"))
            {
                job = JobBuilder.Create<SecondJob3>()
                    .WithIdentity("secondJob3", "someGroup")
                    .Build();

                job.JobDataMap.Put("user", new SecondJobParameter { UserName = name, Password = password });

                if (await scheduler.CheckExists(job.Key))
                {
                    Console.WriteLine("Job already exists!");
                    await scheduler.DeleteJob(job.Key);
                }

                triggerName = "secondJobTrigger3";
            }
            else
            {
                job = JobBuilder.Create<SecondJob4>()
                    .WithIdentity("secondJob4", "someGroup")
                    .Build();

                job.JobDataMap.Put("user", new SecondJobParameter { UserName = name, Password = password });

                if (await scheduler.CheckExists(job.Key))
                {
                    Console.WriteLine("Job already exists!");
                    await scheduler.DeleteJob(job.Key);
                }

                triggerName = "secondJobTrigger4";
            }

            var trigger = TriggerBuilder.Create()
                .UsingJobData("triggerparam", "Some trigger param")
                .WithIdentity(triggerName, "triggerGroup")
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromSeconds(2))
                    .RepeatForever()
                )                
                .Build();            

            await scheduler.ScheduleJob(job, trigger);
            Console.WriteLine("Job has been recreated!");
        }

        public string[] GetPropsMethod()
        {
            var props = new string[2];
            Console.WriteLine("Введите имя");
            props[0] = Console.ReadLine();
            Console.WriteLine("Введите пароль");
            props[1] = Console.ReadLine();
            return props;
        }

        public string Title => "ReInit";
    }
}
