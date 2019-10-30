using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzProject.Jobs
{
    //When this exception is caught, a JobExecutionException is thrown and set to ensure that quartz never runs the job again.
    [DisallowConcurrentExecution]
    public class ErrorJob2 : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine("Error job has been started");
                int zero = 0;
                int calculation = 4815 / zero;
            }
            catch (Exception e)
            {
                JobExecutionException e2 =
                    new JobExecutionException(e);
                e2.UnscheduleAllTriggers = true;
                throw e2;
            }
        }
    }
}
