using Quartz;

namespace QuartzProject.Actions
{
    public interface IAction
    {
        void Execute(IScheduler scheduler, params object[] props);       
        string Title { get; }
        bool ArePropsNeeded { get; }
    }
}
