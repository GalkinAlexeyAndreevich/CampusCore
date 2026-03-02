namespace CampusCore.Services.StudentGroups.Repositories.Models;

public class StudentGroupDb(
	Guid id,
	String name,
	String abbreviation,
	String trainingFormat,
	Int32 studyStartYear,
	Int32 studyEndYear
)
{
	public Guid Id { get; set; } = id;
	public String Name { get; set; } = name;
	public String Abbreviation { get; set; } = abbreviation;
	public String TrainingFormat { get; set; } = trainingFormat;
	public Int32 StudyStartYear { get; set; } = studyStartYear;
	public Int32 StudyEndYear { get; set; } = studyEndYear;
}

