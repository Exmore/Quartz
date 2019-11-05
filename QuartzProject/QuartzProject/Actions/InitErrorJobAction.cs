using Quartz;
using QuartzProject.Jobs;
using System;

namespace QuartzProject.Actions
{
    public class InitErrorJobAction : IAction
    {
        public string Title => "InitErrorJob";

        public async void Execute(IScheduler scheduler)
        {
            var triggerName = "tr1";

            IJobDetail job=  JobBuilder.Create<ErrorJob>()
                    .WithIdentity("simpleJob", "someGroup")
                    .Build();

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
    }

    public class InitErrorJobAction2 : IAction
    {
        public string Title => "InitErrorJob2";

        public bool ArePropsNeeded => false;

        public async void Execute(IScheduler scheduler)
        {
            var triggerName = "tr1";

            IJobDetail job = JobBuilder.Create<ErrorJob2>()
                    .WithIdentity("simpleJob", "someGroup")
                    .Build();

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
    }
}
