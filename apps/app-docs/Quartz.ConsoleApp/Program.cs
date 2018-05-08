using System;
using Quartz.ConsoleApp.tutorial;

namespace Quartz.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RunSchedulerListeners();
        }

        private static void RunSchedulerListeners()
        {
            tutorial.SchedulerListeners.RunMain().GetAwaiter().GetResult();
        }

        public static void RunTriggerListeners()
        {
            TriggerListeners.RunMain().GetAwaiter().GetResult();
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
