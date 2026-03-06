using CampusCore.Domain.Students;

namespace CampusCore.Services.Students.Repositories.Interfaces;

public interface IStudentNameStatisticsRepository
{
    void SaveStatistic(StudentNameStatistic statistic);
    Boolean HasForDate(DateOnly statisticDate);
}

