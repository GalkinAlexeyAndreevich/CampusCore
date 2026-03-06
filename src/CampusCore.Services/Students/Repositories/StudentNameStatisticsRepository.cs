using CampusCore.Domain.Students;
using CampusCore.Services.Students.Repositories.Converters;
using CampusCore.Services.Students.Repositories.Interfaces;
using CampusCore.Services.Students.Repositories.Models;
using CampusCore.Services.Students.Repositories.Queries;
using CampusCore.Tools.Utils;

namespace CampusCore.Services.Students.Repositories;

public class StudentNameStatisticsRepository : IStudentNameStatisticsRepository
{
    public void SaveStatistic(StudentNameStatistic statistic)
    {
        DatabaseUtils.Execute(
            Sql.SaveStudentNameStatistic,
            (parameters) =>
            {
                parameters.AddWithValue("p_statistic_date", statistic.StatisticDate);
                parameters.AddWithValue("p_name", statistic.Name);
                parameters.AddWithValue("p_repeat_count", statistic.RepeatCount);
                parameters.AddWithValue("p_created_at", statistic.CreatedAt);
            }
        );
    }

    public Boolean HasForDate(DateOnly statisticDate)
    {
        Int32? found = DatabaseUtils.Get<Int32?>(
            Sql.HasStudentNameStatisticByDate,
            (parameters) =>
            {
                parameters.AddWithValue("p_statistic_date", statisticDate);
            },
            (reader) => reader.GetInt32(0)
        );

        return found.HasValue;
    }
}

