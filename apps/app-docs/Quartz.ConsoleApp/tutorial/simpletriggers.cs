using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz.ConsoleApp.Jobs;
using Quartz.Impl;

namespace Quartz.ConsoleApp.tutorial
{
    /// <summary>
    /// https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/simpletriggers.html
    /// </summary>
    public class SimpleTriggers
    {
        public void RunMain()
        {

        }

        // Build a trigger for a specific moment in time, with no repeats:
        public void BuildTrigger1()
        {
            // trigger builder creates simple trigger by default, actually an ITrigger is returned
            DateTimeOffset myStartTime = DateTimeOffset.UtcNow;
            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartAt(myStartTime) // some Date 
                .ForJob("job1", "group1") // identify job with name, group strings
                .Build();
        }

        // Build a trigger for a specific moment in time, then repeating every ten seconds ten times:
        public void BuildTrigger2()
        {
            DateTimeOffset myTimeToStartFiring = DateTimeOffset.UtcNow;
            JobKey myJob = JobKey.Create("JobKey-name", "JobKey-group");
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger3", "group1")
                .StartAt(myTimeToStartFiring) // if a start time is not given (if this line were omitted), "now" is implied
                .WithSimpleSchedule(x =>
                    x.WithIntervalInSeconds(10)
                        .WithRepeatCount(10)) // note that 10 repeats will give a total of 11 firings
                .ForJob(myJob) // identify job with handle to its JobDetail itself                   
                .Build();
        }

        // Build a trigger that will fire once, five minutes in the future:
        public void BuildTrigger3()
        {
            JobKey myJobKey = JobKey.Create("JobKey-name", "JobKey-group");
            ITrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
                .WithIdentity("trigger5", "group1")
                .StartAt(DateBuilder.FutureDate(5,
                    IntervalUnit.Minute)) // use DateBuilder to create a date in the future
                .ForJob(myJobKey) // identify job with its JobKey
                .Build();
        }

        // Build a trigger that will fire now, then repeat every five minutes, until the hour 22:00:
        public void BuildTrigger4()
        {
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger7", "group1")
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever())
                .EndAt(DateBuilder.DateOf(22, 0, 0))
                .Build();

        }

        // Build a trigger that will fire at the top of the next hour, then repeat every 2 hours, forever:
        public async Task BuildTrigger5()
        {
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger8") // because group is not specified, "trigger8" will be in the default group
                .StartAt(DateBuilder.EvenHourDate(null)) // get the next even-hour (minutes and seconds zero ("00:00"))
                .WithSimpleSchedule(x => x.WithIntervalInHours(2).RepeatForever())
                // note that in this example, 'forJob(..)' is not called 
                //  - which is valid if the trigger is passed to the scheduler along with the job  
                .Build();
            // construct a scheduler factory
            NameValueCollection props = new NameValueCollection
            {
                {"quartz.serializer.type", "binary"}
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);

            // get a scheduler
            IScheduler scheduler = await factory.GetScheduler();

            IJobDetail job = JobBuilder.Create<DumbJob>()
                .WithIdentity("myJob", "group1") // name "myJob", group "group1"
                .UsingJobData("jobSays", "Hello World!")
                .UsingJobData("myFloatValue", 3.141f)
                .UsingJobData("guid", Guid.NewGuid().ToString())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        // When building SimpleTriggers, you specify the misfire instruction as part of the simple schedule (via SimpleSchedulerBuilder):
        public void BuildTrigger6()
        {
            ITrigger trigger = TriggerBuilder.Create()
                  .WithIdentity("trigger7", "group1")
                  .WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever()
                      .WithMisfireHandlingInstructionNextWithExistingCount())
                  .Build();

        }
    }
}
