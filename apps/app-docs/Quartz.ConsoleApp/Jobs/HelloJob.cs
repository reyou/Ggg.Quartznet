using System;
using System.Threading.Tasks;

namespace Quartz.ConsoleApp.Jobs
{
    public class HelloJob : IJob
    {
        /// <summary>
        /// When the Job’s trigger fires (more on that in a moment), the Execute(..) 
        /// method is invoked by one of the scheduler’s worker threads. 
        /// The JobExecutionContext object that is passed to this method provides the 
        /// job instance with information about its “run-time” environment - a handle 
        /// to the Scheduler that executed it, a handle to the Trigger that triggered 
        /// the execution, the job’s JobDetail object, and a few other items.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }

}
