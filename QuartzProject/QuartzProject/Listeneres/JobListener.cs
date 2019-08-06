using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzProject.Listeneres
{
    public class JobListener : IJobListener
    {
        private void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public string Name => "SomeJob";

        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("JobExecutionVetoed");
        }

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("JobToBeExecuted");
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("JobWasExecuted");
        }
    }
}
