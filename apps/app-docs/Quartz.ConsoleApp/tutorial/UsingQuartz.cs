using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz.ConsoleApp.Jobs;
using Quartz.Impl;

namespace Quartz.ConsoleApp.tutorial
{
    /// <summary>
    /// https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/using-quartz.html
    /// </summary>
    public class UsingQuartz
    {
        public async Task RunMain()
        {
            // construct a scheduler factory
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);

            // get a scheduler
            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(2)
                    .RepeatForever())
                .Build();

            await sched.ScheduleJob(job, trigger);

        }
    }
}
