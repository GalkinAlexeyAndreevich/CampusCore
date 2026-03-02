using CampusCore.Domain.StudentGroups;

namespace CampusCore.Services.StudentGroups.Repositories.Interfaces;

public interface IStudentGroupsRepository
{
	void SaveStudentGroup(StudentGroupBlank studentGroupBlank);
	StudentGroup[] GetAllStudentGroups();
	StudentGroup? GetStudentGroup(Guid groupId);
	void MarkStudentGroupAsDeleted(Guid groupId);
}

