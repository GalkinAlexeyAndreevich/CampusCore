namespace CampusCore.Services.Students.Repositories.Models;

public class StudentNameStatisticDb(Guid id, DateOnly statisticDate, String name, Int32 repeatCount, DateTime createdAt)
{
    public Guid Id { get; set; } = id;
    public DateOnly StatisticDate { get; set; } = statisticDate;
    public String Name { get; set; } = name;
    public Int32 RepeatCount { get; set; } = repeatCount;
    public DateTime CreatedAt { get; set; } = createdAt;
}