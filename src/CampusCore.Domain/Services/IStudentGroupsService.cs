using CampusCore.Domain.StudentGroups;
using CampusCore.Tools.Types.Results;

namespace CampusCore.Domain.Services;

public interface IStudentGroupsService
{
    Result SaveStudentGroup(StudentGroupBlank studentGroupBlank);
    StudentGroup[] GetAllStudentGroups();
    StudentGroup? GetStudentGroup(Guid groupId);
    Result MarkStudentGroupAsDeleted(Guid groupId);
}