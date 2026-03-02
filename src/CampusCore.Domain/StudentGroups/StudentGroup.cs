namespace CampusCore.Domain.StudentGroups;

public class StudentGroup(Guid id, String name, String abbreviation, String trainingFormat, Int32 studyStartYear, Int32 studyEndYear)
{
	public Guid Id { get; } = id;
	public String Name { get; } = name;
	public String Abbreviation { get; } = abbreviation;
	public String TrainingFormat { get; } = trainingFormat;
	public Int32 StudyStartYear { get; } = studyStartYear;
	public Int32 StudyEndYear { get; } = studyEndYear;
}
