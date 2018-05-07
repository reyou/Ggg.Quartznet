using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.ConsoleApp.tutorial
{
    /// <summary>
    /// https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/crontriggers.html
    /// </summary>
    public class CronTriggers
    {
        // Build a trigger that will fire every other minute, between 8am and 5pm, every day:
        public void BuildTrigger1()
        {
            ITrigger trigger = TriggerBuilder.Create()
                 .WithIdentity("trigger3", "group1")
                 .WithCronSchedule("0 0/2 8-17 * * ?")
                 .ForJob("myJob", "group1")
                 .Build();

        }

        // Build a trigger that will fire daily at 10:42 am:
        public void BuildTrigger2()
        {
            // we use CronScheduleBuilder's static helper methods here
            JobKey myJobKey = JobKey.Create("JobKey-name", "JobKey-group");
            ITrigger trigger = TriggerBuilder.Create()
                  .WithIdentity("trigger3", "group1")
                  .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(10, 42))
                  .ForJob(myJobKey)
                  .Build();

            // or -
            ITrigger trigger2 = TriggerBuilder.Create()
                 .WithIdentity("trigger3", "group1")
                 .WithCronSchedule("0 42 10 * * ?")
                 .ForJob("myJob", "group1")
                 .Build();

        }

        // Build a trigger that will fire on Wednesdays at 10:42 am, in a TimeZone other than the system’s default:
        public void BuildTrigger3()
        {
            // we use CronScheduleBuilder's static helper methods here
            JobKey myJobKey = JobKey.Create("JobKey-name", "JobKey-group");
            ITrigger trigger = TriggerBuilder.Create()
                  .WithIdentity("trigger3", "group1")
                  .WithSchedule(CronScheduleBuilder.WeeklyOnDayAndHourAndMinute(DayOfWeek.Wednesday, 10, 42)
                      .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time")))
                  .ForJob(myJobKey)
                  .Build();
            // or -
            ITrigger trigger2 = TriggerBuilder.Create()
                .WithIdentity("trigger3", "group1")
                .WithCronSchedule("0 42 10 ? * WED", x => x.InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time")))
                .ForJob(myJobKey)
                .Build();

        }

        // When building CronTriggers, you specify the misfire instruction as part of 
        // the cron schedule (via WithCronSchedule extension method):
        public void BuildTrigger4()
        {
            // we use CronScheduleBuilder's static helper methods here
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger3", "group1")
                .WithCronSchedule("0 0/2 8-17 * * ?", x => x
                    .WithMisfireHandlingInstructionFireAndProceed())
                .ForJob("myJob", "group1")
                .Build();

        }

    }
}
