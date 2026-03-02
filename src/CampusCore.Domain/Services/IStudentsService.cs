using CampusCore.Domain.Students;
using CampusCore.Tools.Types.Results;

namespace CampusCore.Domain.Services;

public interface IStudentsService
{
    Result SaveStudent(StudentBlank studentBlank);
    Student[] GetAllStudents();
    Student? GetStudent(Guid studentId);
    Result MarkStudentAsDeleted(Guid studentId);
}