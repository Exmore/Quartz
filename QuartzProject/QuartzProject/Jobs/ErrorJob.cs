using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzProject.Jobs
{
    //When this exception is caught, a JobExecutionException is thrown and set to refire the job immediatly.
    [DisallowConcurrentExecution]
    public class ErrorJob : IJob
    {
        // Можно через мэпу изменять
        private static int retriesNumber = 0;
        private int maximumRetries = 100;
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

                retriesNumber++;
                if (retriesNumber >= maximumRetries)
                {
                    e2.UnscheduleAllTriggers = true;
                }
                else
                {
                    e2.RefireImmediately = true;
                }

                throw e2;
            }
        }
    }
}
