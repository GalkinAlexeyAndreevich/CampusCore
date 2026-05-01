namespace CampusCore.Services.Students.Repositories.Models;

public class StudentNameStatisticDb(DateOnly statisticDate, String name, Int32 repeatCount, DateTime createdAt)
{
    public DateOnly StatisticDate { get; set; } = statisticDate;
    public String Name { get; set; } = name;
    public Int32 RepeatCount { get; set; } = repeatCount;
    public DateTime CreatedAt { get; set; } = createdAt;
}