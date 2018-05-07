using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quartz.ConsoleApp.Jobs
{
    public class DumbJobMerged : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                JobKey key = context.JobDetail.Key;
                JobDataMap dataMap = context.MergedJobDataMap;  // Note the difference from the previous example
                string jobSays = dataMap.GetString("jobSays");
                float myFloatValue = dataMap.GetFloat("myFloatValue");
                IList<DateTimeOffset> state = (IList<DateTimeOffset>)dataMap["myStateData"];
                state.Add(DateTimeOffset.UtcNow);

                await Console.Error.WriteLineAsync("Instance " + key + " of DumbJobMerged says: " + jobSays + ", and val is: " + myFloatValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

}
