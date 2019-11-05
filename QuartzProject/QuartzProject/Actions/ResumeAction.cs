using System;
using Quartz;
using QuartzProject.Jobs;

namespace QuartzProject.Actions
{
    public class ResumeAction : IAction
    {
        public async void Execute(IScheduler scheduler)
        {
            var job = GetJobDetail();

            if (await scheduler.CheckExists(job.Key))
            {
                Console.WriteLine("Job already exists!");
                await scheduler.ResumeJob(job.Key);
            }
        }

        private IJobDetail GetJobDetail()
        {
            Console.WriteLine("1 2 3 4 5?");

            if (Console.ReadLine().Equals("1"))
            {
                IJobDetail job = JobBuilder.Create<SimpleJob>()
                    .WithIdentity("simpleJob", "someGroup")
                    .Build();
                return job;
            }
            else if (Console.ReadLine().Equals("2"))
            {
                IJobDetail job = JobBuilder.Create<SecondJob>()
                    .WithIdentity("secondJob", "someGroup")
                    .Build();
                return job;
            }
            else if (Console.ReadLine().Equals("3"))
            {
                IJobDetail job = JobBuilder.Create<SecondJob>()
                    .WithIdentity("secondJob", "someGroup")
                    .Build();
                return job;
            }
            else if (Console.ReadLine().Equals("4"))
            {
                IJobDetail job = JobBuilder.Create<SecondJob>()
                    .WithIdentity("secondJob", "someGroup")
                    .Build();
                return job;
            }
            else
            {
                IJobDetail job = JobBuilder.Create<SecondJob>()
                    .WithIdentity("secondJob", "someGroup")
                    .Build();
                return job;
            }
        }

        public string Title => "Resume";
    }
}
