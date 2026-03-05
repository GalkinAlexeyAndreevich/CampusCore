using CampusCore.Domain.Services;
using CampusCore.Domain.StudentGroups;
using CampusCore.Domain.Students;

namespace CampusCore.Services.Students;

public class StudentScholarshipCalculator : IStudentScholarshipCalculator
{
    private const decimal MIN_AVERAGE_GRADE_SCHOLARSHIP = 4m;

    public StudentScholarship Calculate(Student student, StudentGroup? group, DateTime now)
    {
        if (group is null || student.AverageGrade < MIN_AVERAGE_GRADE_SCHOLARSHIP)
            return new StudentScholarship(student.Id, 0);

        Int32 course = CalcCourseSafe(group, now);

        Double scholarship = course == 0
            ? 0
            : (Double)(student.AverageGrade * 500) * Math.Sqrt(course);

        return new StudentScholarship(student.Id, scholarship);
    }

    private static Int32 CalcCourseSafe(StudentGroup group, DateTime now)
    {
      Int32 academicYearStartYear = now.Month >= 9 ? now.Year : now.Year - 1;

      if (academicYearStartYear < group.StudyStartYear) return 0;

      Int32 course = academicYearStartYear - group.StudyStartYear + 1;
      Int32 lastCourse = Math.Max(1, group.StudyEndYear - group.StudyStartYear);

      if (course < 1) return 0;
      if (course > lastCourse) return 0;

      return course;
    }
}

