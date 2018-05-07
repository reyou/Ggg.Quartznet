using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz.ConsoleApp.Jobs;
using Quartz.ConsoleApp.Logging;
using Quartz.Impl;

namespace Quartz.ConsoleApp.QuartzSampleApp
{
    /// <summary>
    /// https://www.quartz-scheduler.net/documentation/quartz-3.x/quick-start.html
    /// </summary>
    public class Program
    {
        public static void RunMain()
        {
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());
            // trigger async evaluation
            RunProgram().GetAwaiter().GetResult();

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }

        private static async Task RunProgram(bool shutDown = false)
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                Console.WriteLine("Grab the Scheduler instance from the Factory");
                NameValueCollection props = new NameValueCollection
                {
                    {"quartz.serializer.type", "binary"}
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();

                // and start it off
                Console.WriteLine("and start it off");
                await scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .Build();

                // Tell quartz to schedule the job using our trigger
                await scheduler.ScheduleJob(job, trigger);

                // some sleep to show what's happening
                Console.WriteLine("some sleep to show what's happening");
                await Task.Delay(TimeSpan.FromSeconds(5));

                // and last shut down the scheduler when you are ready to close your program
                Console.WriteLine("and last shut down the scheduler when you are ready to close your program");
                if (shutDown)
                {
                    /*Once you obtain a scheduler using StdSchedulerFactory.GetDefaultScheduler(), your
                 application will not terminate by default until you call scheduler.Shutdown(), 
                 because there will be active threads (non-daemon threads).*/
                    await scheduler.Shutdown();
                }
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se.ToString());
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }
    }
}
