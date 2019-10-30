using System;
using Quartz;
using QuartzProject.Jobs;

namespace QuartzProject.Actions
{
    public class DeleteAction : IAction
    {
        public async void Execute(IScheduler scheduler, params object[] props)
        {
            var job = GetJobDetail();

            if (await scheduler.CheckExists(job.Key))
            {
                await scheduler.DeleteJob(job.Key);
                Console.WriteLine("Job has been deleted!");
            }
        }

        private IJobDetail GetJobDetail()
        {
            Console.WriteLine("1 or 2?");

            if (Console.ReadLine().Equals("1"))
            {
                IJobDetail job = JobBuilder.Create<SimpleJob>()
                    .WithIdentity("simpleJob", "someGroup")
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

        public string Title => "Delete";
        public bool ArePropsNeeded => false;
    }
}
