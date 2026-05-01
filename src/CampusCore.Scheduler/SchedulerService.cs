using CampusCore.Scheduler.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CampusCore.Scheduler;

public static class SchedulerService
{
    public static void AddScheduler(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            JobKey jobKey = new(nameof(GetAndSaveStudentNameStatisticJob));

            q.AddJob<GetAndSaveStudentNameStatisticJob>(options => options.WithIdentity(jobKey));

            q.AddTrigger(options => options
                  .ForJob(jobKey)
                  .WithIdentity($"{jobKey.Name}.trigger")
                  // Каждый день в 02:00 ночи
                  .WithCronSchedule("0 0 2 * * ?")
            );
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
    }
}

