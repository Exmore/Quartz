using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzProject.Listeneres
{
    public class TriggerListener : ITriggerListener
    {
        public string Name => "SomeTrigger";

        private void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public async Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("TriggerComplete");
        }

        public async Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("TriggerFired");
        }

        public async Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("TriggerMisfired");
        }

        public Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            Write("VetoJobExecution");
            return null;
        }
    }
}
