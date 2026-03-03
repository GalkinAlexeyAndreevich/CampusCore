using CampusCore.Domain.StudentGroups.Enums;

namespace CampusCore.Domain.StudentGroups;

public class StudentGroup(Guid id, String name, String abbreviation, TrainingFormat trainingFormat, Int32 studyStartYear, Int32 studyEndYear)
{
	public Guid Id { get; } = id;
	public String Name { get; } = name;
	public String Abbreviation { get; } = abbreviation;
	public TrainingFormat TrainingFormat { get; } = trainingFormat;
	public Int32 StudyStartYear { get; } = studyStartYear;
	public Int32 StudyEndYear { get; } = studyEndYear;
}
