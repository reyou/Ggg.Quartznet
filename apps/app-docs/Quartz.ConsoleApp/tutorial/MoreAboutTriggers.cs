using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz.ConsoleApp.Jobs;
using Quartz.Impl;
using Quartz.Impl.Calendar;

namespace Quartz.ConsoleApp.tutorial
{
    /// <summary>
    /// https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/more-about-triggers.html
    /// </summary>
    public class MoreAboutTriggers
    {
        public async Task RunMain()
        {
            HolidayCalendar cal = new HolidayCalendar();
            DateTime someDate = DateTime.Now.AddDays(2);
            cal.AddExcludedDate(someDate);
            // construct a scheduler factory
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);

            // get a scheduler
            IScheduler sched = await factory.GetScheduler();
            await sched.AddCalendar("myHolidays", cal, false, false);

            ITrigger t = TriggerBuilder.Create()
                .WithIdentity("myTrigger")
                .ForJob("myJob")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(9, 30)) // execute job daily at 9:30
                .ModifiedByCalendar("myHolidays") // but not on holidays
                .Build();

            // define the job and tie it to our DumbJob class
            IJobDetail job = JobBuilder.Create<DumbJob>()
                .WithIdentity("myJob", "group1") // name "myJob", group "group1"
                .UsingJobData("jobSays", "Hello World!")
                .UsingJobData("myFloatValue", 3.141f)
                .UsingJobData("guid", Guid.NewGuid().ToString())
                .Build();

            await sched.ScheduleJob(job, t);

            ITrigger t2 = TriggerBuilder.Create()
                .WithIdentity("myTrigger2")
                .ForJob("myJob2")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(11, 30)) // execute job daily at 11:30
                .ModifiedByCalendar("myHolidays") // but not on holidays
                .Build();

            // .. schedule job with trigger2 
            await sched.ScheduleJob(job, t2);

        }
    }
}
