using CampusCore.Domain.Students.Enums;

namespace CampusCore.Services.Students.Repositories.Models;

public class StudentDb(
    Guid id,
    String lastName,
    String firstName,
    String? patronymic,
    Gender gender,
    DateTime dateOfBirth,
    decimal averageGrade,
    String[]? specialNotes,
    Guid groupId
)
{
    public Guid Id { get; set; } = id;
    public String LastName { get; set; } = lastName;
    public String FirstName { get; set; } = firstName;
    public String? Patronymic { get; set; } = patronymic;
    public Gender Gender { get; set; } = gender;
    public DateTime DateOfBirth { get; set; } = dateOfBirth;
    public decimal AverageGrade { get; set; } = averageGrade;
    public String[]? SpecialNotes { get; set; } = specialNotes;
    public Guid GroupId { get; set; } = groupId;
}