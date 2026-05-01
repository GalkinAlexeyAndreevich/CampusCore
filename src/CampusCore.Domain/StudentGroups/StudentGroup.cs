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
	
	public Int32 CalcCourseSafe()
	{
		DateTime now = DateTime.Now;
		Int32 academicYearStartYear = now.Month >= 9 ? now.Year : now.Year - 1;

		if (academicYearStartYear < StudyStartYear) return 0;

		Int32 course = academicYearStartYear - StudyStartYear + 1;
		Int32 lastCourse = Math.Max(1, StudyEndYear - StudyStartYear);

		if (course < 1) return 0;
		if (course > lastCourse) return 0;

		return course;
	}
}
