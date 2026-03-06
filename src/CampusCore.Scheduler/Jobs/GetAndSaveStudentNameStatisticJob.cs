using Quartz;

namespace CampusCore.Scheduler.Jobs;

public class GetAndSaveStudentNameStatisticJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Job executed: " + DateTime.Now);
        return Task.CompletedTask;
    }
}