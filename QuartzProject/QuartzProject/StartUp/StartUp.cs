using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

namespace QuartzProject.StartUp
{
    public static class StartUp
    {
        public static IScheduler ConfigurateScheduler(string instanceName)
        {
            NameValueCollection props = new NameValueCollection();

            props["quartz.serializer.type"] = "json";
            props["quartz.scheduler.instanceName"] = instanceName;//"TestScheduler";


            //props["quartz.scheduler.instanceId"] = instanceName;//"instance_one";

            props["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            // Нужно понять как поставить true
            props["quartz.jobStore.useProperties"] = "false";
            props["quartz.jobStore.dataSource"] = "default";
            props["quartz.jobStore.tablePrefix"] = "QRTZ_";
            props["quartz.dataSource.default.connectionString"] = "Data Source=localhost:1521/xe;User Id=NewUser;Password=123456;";
            props["quartz.dataSource.default.provider"] = "OracleODP";
            props["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.OracleDelegate, Quartz";

            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            var scheduler = factory.GetScheduler().Result;

            return scheduler;
        }
    }
}
