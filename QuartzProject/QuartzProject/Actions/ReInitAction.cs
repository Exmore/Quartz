using System;
using Quartz;
using QuartzProject.Jobs;
using QuartzProject.Models;

namespace QuartzProject.Actions
{
    public class ReInitAction : IAction
    {
        private IJobDetail GetJobDetail()
        {
            return null;
        }

        public async void Execute(IScheduler scheduler, params object[] props)
        {
            var name = props.Length > 0 ? props[0].ToString() : "Vasyan";
            var password = props.Length > 1 ? props[1].ToString() : "VasyanPasswords";

            Console.WriteLine("1 or 2?");

            IJobDetail job;

            if (Console.ReadLine().Equals("1"))
            {
                job = JobBuilder.Create<SimpleJob>()
                    .WithIdentity("simpleJob", "someGroup")
                    .Build();

                job.JobDataMap.Put("user", new SimpleJobParameter { UserName = name, Password = password });
                Ex(scheduler, job);
            }
            else
            {
                job = JobBuilder.Create<SecondJob>()
                    .WithIdentity("secondJob", "someGroup")
                    .Build();

                job.JobDataMap.Put("user", new SecondJobParameter { UserName = name, Password = password });
                Ex(scheduler, job, "secondTrigger");
            }


        }

        private async void Ex(IScheduler scheduler, IJobDetail job, string triggerName = "someTrigger")
        {
            if (await scheduler.CheckExists(job.Key))
            {
                Console.WriteLine("Job already exists!");

                //Если захотим по-новой создать, нужно сначал удалить, иначе будет краш бд
                await scheduler.DeleteJob(job.Key);

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
            else
            {
                Console.WriteLine("It's ok. You can schedule this job.");

                var trigger = TriggerBuilder.Create()
                    .UsingJobData("triggerparam", "Some trigger param")
                    .WithIdentity(triggerName, "triggerGroup")
                    .WithSimpleSchedule(x => x
                        .WithInterval(TimeSpan.FromSeconds(2))
                        .RepeatForever()
                    )
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
        }

        public string Title => "ReInit";
        public bool ArePropsNeeded => true;
    }
}
