using Quartz;

namespace QuartzProject.Actions
{
    public interface IAction
    {
        void Execute(IScheduler scheduler);
        string Title { get; }
    }
}
