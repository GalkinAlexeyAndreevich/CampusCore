namespace CampusCore.Domain.Students;

public sealed record StudentNameStatistic(
    DateOnly StatisticDate,
    String Name,
    Int32 RepeatCount,
    DateTime CreatedAt
);

