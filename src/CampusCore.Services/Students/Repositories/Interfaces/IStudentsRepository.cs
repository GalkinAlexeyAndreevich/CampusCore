using CampusCore.Domain.Students;

namespace CampusCore.Services.Students.Repositories.Interfaces;

public interface IStudentsRepository
{
	void SaveStudent(StudentBlank studentBlank);
	Student[] GetAllStudents();
	StudentDetail[] GetAllStudentsDetailed();
	Student[] GetStudentsByIds(Guid[] studentIds);
	Student? GetStudent(Guid studentId);
	StudentCountOnGroup[] GetStudentsCountOnGroupIds(Guid[] groupIds);
	void MarkStudentAsDeleted(Guid studentId);
}