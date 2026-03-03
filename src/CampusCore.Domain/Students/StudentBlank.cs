using CampusCore.Domain.Students.Enums;

namespace CampusCore.Domain.Students;

public class StudentBlank
{
	public Guid? Id { get; set; }
	public String? LastName { get; set; }
	public String? FirstName { get; set; }
	public String? Patronymic { get; set; }
	public Gender? Gender { get; set; }
	public DateTime? DateOfBirth { get; set; }
	public decimal? AverageGrade { get; set; } 
	public String[]? SpecialNotes { get; set; }
	public Guid? GroupId { get; set; } 
}
