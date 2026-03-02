namespace CampusCore.Domain.Students;

public class StudentBlank
{
	public Guid? Id { get; set; }
	public String? LastName { get; set; }
	public String? FirstName { get; set; }
	public String? Patronymic { get; set; }
	public String? Gender { get; set; }
	public Int32? Age { get; set; }
	public decimal? AverageGrade { get; set; } 
	public String[]? SpecialNotes { get; set; }
	public Guid? GroupId { get; set; } 
}
