using CampusCore.Domain.StudentGroups;
using CampusCore.Tools.Types.Results;

namespace CampusCore.Domain.Services;

public interface IStudentGroupService
{
    Result SaveStudentGroup(StudentGroupBlank studentGroupBlank);
    StudentGroup[] GetAllStudentGroup();
    StudentGroup? GetStudentGroup(Guid groupId);
    Result MarkStudentGroupAsDeleted(Guid groupId);
}