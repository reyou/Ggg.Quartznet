using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz.ConsoleApp.Jobs;
using Quartz.Impl;

namespace Quartz.ConsoleApp.tutorial
{
    /// <summary>
    /// https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/more-about-jobs.html
    /// </summary>
    public class MoreAboutJobsInjected
    {
        public async Task RunMain()
        {
            try
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
                IList<DateTimeOffset> myStateData = new List<DateTimeOffset>();
                // ReSharper disable once UseObjectOrCollectionInitializer
                JobDataMap newJobDataMap = new JobDataMap();
                newJobDataMap.Add("JobSays", "MoreAboutJobsInjected running...");
                newJobDataMap.Add("myStateData", myStateData);
                newJobDataMap.Add("FloatValue", 3.141f);
                IJobDetail job = JobBuilder.Create<DumbJobInjected>()
                    .WithIdentity("myJob", "group1") // name "myJob", group "group1"
                    .UsingJobData("guid", Guid.NewGuid().ToString())
                    .SetJobData(newJobDataMap)
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
