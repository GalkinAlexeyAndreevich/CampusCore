using CampusCore.Domain.Students;
using CampusCore.Services.Students.Repositories.Models;
using Npgsql;

namespace CampusCore.Services.Students.Repositories.Converters;

internal static class StudentNameStatisticsConverter
{
    internal static StudentNameStatistic[] ToStudentNameStatistics(this StudentNameStatisticDb[] statisticDbs) =>
        [.. statisticDbs.Select(ToStudentNameStatistic)];

    internal static StudentNameStatistic ToStudentNameStatistic(this StudentNameStatisticDb statisticDb) =>
        new(statisticDb.StatisticDate, statisticDb.Name, statisticDb.RepeatCount, statisticDb.CreatedAt);

    internal static StudentNameStatisticDb ToStudentNameStatisticDb(this NpgsqlDataReader reader)
    {
        return new StudentNameStatisticDb(
            reader.GetFieldValue<DateOnly>(reader.GetOrdinal("statistic_date")),
            reader.GetString(reader.GetOrdinal("name")),
            reader.GetInt32(reader.GetOrdinal("repeat_count")),
            reader.GetDateTime(reader.GetOrdinal("created_at"))
        );
    }
}

