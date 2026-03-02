using CampusCore.Domain.Students;
using CampusCore.Tools.Types.Results;

namespace CampusCore.Domain.Services;

public interface IStudentService
{
    Result SaveStudent(StudentBlank studentBlank);
    Student[] GetAllStudent();
    Student? GetStudent(Guid studentId);
    Result MarkStudentAsDeleted(Guid studentId);
}