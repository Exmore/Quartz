using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzProject.Listeneres
{
    public class SchedulerListener : ISchedulerListener
    {
        private void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public async Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("JobAdded");
        }

        public async Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("JobDeleted");
        }

        public async Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("JobInterrupted");
        }

        public async Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("JobPaused");
        }

        public async Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("JobResumed");
        }

        public async Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("JobScheduled");
        }

        public async Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("JobsPaused");
        }

        public async Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("JobsResumed");
        }

        public async Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("JobUnscheduled");
        }

        public async Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("SchedulerError");
        }

        public async Task SchedulerInStandbyMode(CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("SchedulerInStandbyMode");
        }

        public async Task SchedulerShutdown(CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("SchedulerShutdown");
        }

        public async Task SchedulerShuttingdown(CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("SchedulerShuttingdown");
        }

        public async Task SchedulerStarted(CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("SchedulerStarted");
        }

        public async Task SchedulerStarting(CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("SchedulerStarting");
        }

        public async Task SchedulingDataCleared(CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("SchedulingDataCleared");
        }

        public async Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("TriggerFinalized");
        }

        public async Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("TriggerPaused");
        }

        public async Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("TriggerResumed");
        }

        public async Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("TriggersPaused");
        }

        public async Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("TriggersResumed");
        }
    }
}
