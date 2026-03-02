using CampusCore.Domain.Students;
using CampusCore.Tools.Types.Results;

namespace CampusCore.Services.Students.Repositories.Interfaces;

public interface IStudentsRepository
{
	Result SaveStudent(StudentBlank studentBlank);
	Student[] GetAllStudents();
	Student? GetStudent(Guid studentId);
	Result MarkStudentAsDeleted(Guid studentId);
}