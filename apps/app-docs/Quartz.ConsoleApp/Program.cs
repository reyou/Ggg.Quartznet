using System;
using Quartz.ConsoleApp.tutorial;

namespace Quartz.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RunTriggerListeners();
        }

        public static void RunSchedulerListeners()
        {
            SchedulerListenersSample.RunMain().GetAwaiter().GetResult();
            Console.ReadLine();
        }

        public static void RunTriggerListeners()
        {
            TriggerListeners.RunMain().GetAwaiter().GetResult();
            Console.ReadLine();
        }

        public static void RunMoreAboutJobsInjected()
        {
            new MoreAboutJobsInjected().RunMain().GetAwaiter().GetResult();
            Console.ReadLine();
        }

        public static void RunMoreAboutJobsMerged()
        {
            new MoreAboutJobsMerged().RunMain().GetAwaiter().GetResult();
            Console.ReadLine();
        }

        public static void RunMoreAboutJobs()
        {
            new MoreAboutJobs().RunMain().GetAwaiter().GetResult();
            Console.ReadLine();
        }

        public static void RunUsingQuartz()
        {
            new UsingQuartz().RunMain().GetAwaiter().GetResult();
            Console.ReadLine();
        }

        public static void RunQuartzSampleApp()
        {
            QuartzSampleApp.Program.RunMain();
        }
    }
}
