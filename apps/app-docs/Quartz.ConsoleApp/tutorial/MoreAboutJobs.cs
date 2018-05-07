using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz.ConsoleApp.Jobs;
using Quartz.Impl;

namespace Quartz.ConsoleApp.tutorial
{
    /// <summary>
    /// https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/more-about-jobs.html
    /// </summary>
    public class MoreAboutJobs
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

            // define the job and tie it to our DumbJob class
            IJobDetail job = JobBuilder.Create<DumbJob>()
                .WithIdentity("myJob", "group1") // name "myJob", group "group1"
                .UsingJobData("jobSays", "Hello World!")
                .UsingJobData("myFloatValue", 3.141f)
                .UsingJobData("guid", Guid.NewGuid().ToString())
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
