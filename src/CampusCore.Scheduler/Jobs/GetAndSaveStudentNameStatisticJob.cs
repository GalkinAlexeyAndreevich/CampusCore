using CampusCore.Domain.Services;
using Quartz;

namespace CampusCore.Scheduler.Jobs;

public class GetAndSaveStudentNameStatisticJob(IStudentsService studentsService) : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Сбор статистики по именам студентов" + DateTime.Now);
        studentsService.InsertStudentNameStatistic();
        return Task.CompletedTask;
    }
}