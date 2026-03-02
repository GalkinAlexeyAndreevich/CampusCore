using CampusCore.Domain.Students;

namespace CampusCore.Services.Students.Repositories.Interfaces;

public interface IStudentsRepository
{
	void SaveStudent(StudentBlank studentBlank);
	Student[] GetAllStudents();
	Student? GetStudent(Guid studentId);
	void MarkStudentAsDeleted(Guid studentId);
}