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
            props["quartz.scheduler.instanceName"] = instanceName;
            
            

            props["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            props["quartz.jobStore.useProperties"] = "false";
            props["quartz.jobStore.dataSource"] = "default";
            props["quartz.jobStore.tablePrefix"] = "QRTZ_";


            props["quartz.scheduler.instanceId"] = "AUTO"; // Чтобы кластер норм работал
            // Для кластера
            props["quartz.jobStore.clustered"] = "true";


            // Для кластра, когда одновременно несколько делают 
            //props["org.quartz.jobStore.isClustered"] = "true";


            //props["org.quartz.threadPool.class"] = "quartz.simpl.SimpleThreadPool";
            //props["org.quartz.threadPool.threadCount"] = "2";
            //props["org.quartz.threadPool.threadPriority"] = "5";            

            props["quartz.dataSource.default.connectionString"] = "Data Source=localhost:1521/xe;User Id=NewUser;Password=123456;";
            props["quartz.dataSource.default.provider"] = "OracleODP";
            props["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.OracleDelegate, Quartz";

            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            var scheduler = factory.GetScheduler().Result;

            return scheduler;
        }
    }
}
