using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz.ConsoleApp.JobListeners;
using Quartz.Impl;
using Quartz.Impl.Matchers;

namespace Quartz.ConsoleApp.tutorial
{
    /// <summary>
    /// Listeners are objects that you create to perform actions based on events 
    /// occuring within the scheduler. As you can probably guess, TriggerListeners 
    /// receive events related to triggers, and JobListeners receive events 
    /// related to jobs.
    /// Listeners are not used by most users of Quartz.NET, but are handy when 
    /// application requirements create the need for the notification of events, 
    /// without the Job itself explicitly notifying the application.
    /// https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/trigger-and-job-listeners.html
    /// </summary>
    public class TriggerListeners
    {
        public static async Task RunMain()
        {
            await AddAJobListener();
        }
        // Adding a JobListener that is interested in a particular job:
        public static async Task AddAJobListener()
        {
            NameValueCollection props = new NameValueCollection
            {
                {"quartz.serializer.type", "binary"}
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            IScheduler scheduler = await factory.GetScheduler();
            IJobListener myJobListener = new GggJobListener();
            scheduler.ListenerManager.AddJobListener(myJobListener, KeyMatcher<JobKey>.KeyEquals(new JobKey("myJobName", "myJobGroup")));
        }

        // Adding a JobListener that is interested in all jobs of a particular group:
        public static async Task AddAJobListener2()
        {
            NameValueCollection props = new NameValueCollection
            {
                {"quartz.serializer.type", "binary"}
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            IScheduler scheduler = await factory.GetScheduler();
            IJobListener myJobListener = new GggJobListener();
            scheduler.ListenerManager.AddJobListener(myJobListener, GroupMatcher<JobKey>.GroupEquals("myJobGroup"));
        }

        // Adding a JobListener that is interested in all jobs of two particular groups:
        public static async Task AddAJobListener3()
        {
            NameValueCollection props = new NameValueCollection
            {
                {"quartz.serializer.type", "binary"}
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            IScheduler scheduler = await factory.GetScheduler();
            IJobListener myJobListener = new GggJobListener();
            scheduler.ListenerManager.AddJobListener(myJobListener,
                OrMatcher<JobKey>.Or(GroupMatcher<JobKey>.GroupEquals("myJobGroup"), GroupMatcher<JobKey>.GroupEquals("yourGroup")));
        }

        // Adding a JobListener that is interested in all jobs:
        public static async Task AddAJobListener4()
        {
            NameValueCollection props = new NameValueCollection
            {
                {"quartz.serializer.type", "binary"}
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            IScheduler scheduler = await factory.GetScheduler();
            IJobListener myJobListener = new GggJobListener();
            scheduler.ListenerManager.AddJobListener(myJobListener, GroupMatcher<JobKey>.AnyGroup());
        }



    }
}
