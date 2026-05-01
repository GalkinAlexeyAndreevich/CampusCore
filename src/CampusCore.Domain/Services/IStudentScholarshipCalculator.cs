using CampusCore.Domain.StudentGroups;
using CampusCore.Domain.Students;

namespace CampusCore.Domain.Services;

public interface IStudentScholarshipCalculator
{
    StudentScholarship Calculate(Student student, StudentGroup? group, DateTime now);
}

