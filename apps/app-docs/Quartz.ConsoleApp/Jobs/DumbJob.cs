using System;
using System.Threading.Tasks;

namespace Quartz.ConsoleApp.Jobs
{
    public class DumbJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string jobSays = dataMap.GetString("jobSays");
            float myFloatValue = dataMap.GetFloat("myFloatValue");
            string guid = dataMap.GetString("guid");
            await Console.Error.WriteLineAsync("Instance " + key + " of DumbJob says: " + jobSays + ", and val is: " + myFloatValue
                                               + ", and guid: " + guid);
        }
    }

}
