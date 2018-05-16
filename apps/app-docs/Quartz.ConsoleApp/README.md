Quartz.NET
https://didyoureadme.azurewebsites.net/UserUrls/TagUrls?UserUrlTagId=5d49fe38-6d00-4018-8844-0461f679ba4a&WillRead=True
//=============================================================================
Frequently Asked Questions
https://www.quartz-scheduler.net/documentation/faq.html
//=============================================================================
Quartz Enterprise Scheduler .NET
https://github.com/quartznet/quartznet
//=============================================================================
Install-Package Quartz
//=============================================================================
SimpleTrigger ALWAYS fires exacly every N seconds, with no relation to the time 
of day.
CronTrigger ALWAYS fires at a given time of day and then computes its next time 
to fire. If that time does not occur on a given day, the trigger will be skipped. 
If the time occurs twice in a given day, it only fires once, because after firing 
on that time the first time, it computes the next time of day to fire on.
//=============================================================================
The key interfaces and classes of the Quartz API are:

IScheduler - the main API for interacting with the scheduler.
Ex: scheduler.ScheduleJob(job, trigger);

IJob - an interface to be implemented by components that you wish to have 
executed by the scheduler.

IJobDetail - used to define instances of Jobs.

ITrigger - a component that defines the schedule upon which a given Job will be 
executed.

JobBuilder - used to define/build JobDetail instances, which define instances 
of Jobs.

TriggerBuilder - used to define/build Trigger instances.
//=============================================================================
The only type of exception that you should throw from the execute method is the 
JobExecutionException. Because of this, you should generally wrap the entire 
contents of the execute method with a ‘try-catch’ block. You should also spend 
some time looking at the documentation for the JobExecutionException, as your 
job can use it to provide the scheduler various directives as to how you want 
the exception to be handled.
//=============================================================================
Configuring Quartz to use RAMJobStore

quartz.jobStore.type = Quartz.Simpl.RAMJobStore, Quartz
To use RAMJobStore (and assuming you’re using StdSchedulerFactory) you don’t 
need to do anything special. Default configuration of Quartz.NET uses 
RAMJobStore as job store implementation.
//=============================================================================
JobStores
JobStore’s are responsible for keeping track of all the “work data” that you 
give to the scheduler: jobs, triggers, calendars, etc. 
https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/job-stores.html
//=============================================================================