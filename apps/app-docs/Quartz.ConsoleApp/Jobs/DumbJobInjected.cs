using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quartz.ConsoleApp.Jobs
{
    /// <summary>
    /// https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/more-about-jobs.html
    /// </summary>
    public class DumbJobInjected : IJob
    {
        public string JobSays { private get; set; }
        public float FloatValue { private get; set; }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                JobKey key = context.JobDetail.Key;
                JobDataMap dataMap = context.MergedJobDataMap;  // Note the difference from the previous example
                IList<DateTimeOffset> state = (IList<DateTimeOffset>)dataMap["myStateData"];
                state.Add(DateTimeOffset.UtcNow);
                await Console.Error.WriteLineAsync("Instance " + key + " of DumbJob says: " + JobSays + ", and val is: " + FloatValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

}
