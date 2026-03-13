using System.Text.RegularExpressions;
using CampusCore.Domain.StudentGroups;
using CampusCore.Domain.Students.Enums;

namespace CampusCore.Domain.Students;

public class StudentDetail(
    Guid id,
    String lastName,
    String firstName,
    String? patronymic,
    Gender gender,
    DateTime dateOfBirth,
    decimal averageGrade,
    String[]? specialNotes,
    Guid groupId,
    StudentGroup? group
)
{
    public Guid Id { get; } = id;
    public String LastName { get; } = lastName;
    public String FirstName { get; } = firstName;
    public String? Patronymic { get; } = patronymic;
    public Gender Gender { get; } = gender;
    public DateTime DateOfBirth { get; } = dateOfBirth;
    public decimal AverageGrade { get; } = averageGrade;
    public String[]? SpecialNotes { get; } = specialNotes;
    public Guid GroupId { get; } = groupId;
    public StudentGroup? group { get; } = group;
}