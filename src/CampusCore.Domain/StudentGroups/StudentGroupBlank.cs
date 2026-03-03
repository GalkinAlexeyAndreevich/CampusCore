using CampusCore.Domain.StudentGroups.Enums;

namespace CampusCore.Domain.StudentGroups;

public class StudentGroupBlank
{
	public Guid? Id { get; set; }
	public String? Name { get; set; }
	public String? Abbreviation { get; set; }
	public TrainingFormat? TrainingFormat { get; set; }
	public Int32? StudyStartYear { get; set; }
	public Int32? StudyEndYear { get; set; }
}
