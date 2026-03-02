namespace CampusCore.Services.Students.Repositories.Models;

public class StudentDb(
    Guid id,
    String lastName,
    String firstName,
    String? patronymic,
    String gender,
    Int32 age,
    decimal averageGrade,
    String[]? specialNotes,
    Guid groupId
)
{
    public Guid Id { get; set; } = id;
    public String LastName { get; set; } = lastName;
    public String FirstName { get; set; } = firstName;
    public String? Patronymic { get; set; } = patronymic;
    public String Gender { get; set; } = gender;
    public Int32 Age { get; set; } = age;
    public decimal AverageGrade { get; set; } = averageGrade;
    public String[]? SpecialNotes { get; set; } = specialNotes;
    public Guid GroupId { get; set; } = groupId;
}