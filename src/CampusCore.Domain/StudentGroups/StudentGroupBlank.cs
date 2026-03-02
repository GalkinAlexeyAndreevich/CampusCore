using CampusCore.Domain.Products.Enums;

namespace CampusCore.Domain.StudentGroups;

public class StudentGroupBlank
{
	public Guid? Id { get; set; }
	public String? Name { get; set; }
	public String? Abbreviation { get; set; }
	public String? TrainingFormat { get; set; }
	public Int32? StudyStartYear { get; set; }
	public Int32? StudyEndYear { get; set; }
}
