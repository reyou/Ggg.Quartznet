using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz.ConsoleApp.SchedulerListeners;
using Quartz.Impl;

namespace Quartz.ConsoleApp.tutorial
{
    /// <summary>
    /// https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/scheduler-listeners.html
    /// SchedulerListeners are much like ITriggerListeners and IJobListeners, except they receive notification 
    /// of events within the scheduler itself - not necessarily events related to a specific trigger or job.
    /// </summary>
    public static class SchedulerListenersSample
    {
        public static async Task RunMain()
        {
            await AddASchedulerListener();
        }

        // Adding a SchedulerListener:
        private static async Task AddASchedulerListener()
        {
            NameValueCollection props = new NameValueCollection
            {
                {"quartz.serializer.type", "binary"}
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            IScheduler scheduler = await factory.GetScheduler();

            ISchedulerListener mySchedListener = new GggSchedulerListener();
            scheduler.ListenerManager.AddSchedulerListener(mySchedListener);
        }

        // Removing a SchedulerListener:
        private static async Task RemoveSchedulerListener()
        {
            NameValueCollection props = new NameValueCollection
            {
                {"quartz.serializer.type", "binary"}
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            IScheduler scheduler = await factory.GetScheduler();

            ISchedulerListener mySchedListener = new GggSchedulerListener();
            scheduler.ListenerManager.RemoveSchedulerListener(mySchedListener);
        }

    }
}
