using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzProject.Jobs
{
    //When this exception is caught, a JobExecutionException is thrown and set to refire the job immediatly.
    [DisallowConcurrentExecution]
    public class ErrorJob : IJob
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
                e2.RefireImmediately = true;
                throw e2;
            }
        }
    }
}
