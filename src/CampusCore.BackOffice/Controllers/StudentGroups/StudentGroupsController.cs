using CampusCore.Domain.Services;
using CampusCore.Domain.StudentGroups;
using CampusCore.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace CampusCore.BackOffice.Controllers.StudentGroups;

public class StudentGroupsController(IStudentGroupsService studentGroupsService) : AppController
{
	[HttpPost("student-groups/save")]
	public Result SaveStudentGroup([FromBody] StudentGroupBlank studentGroupBlank)
	{
		return studentGroupsService.SaveStudentGroup(studentGroupBlank);
	}

	[HttpGet("student-groups")]
	public StudentGroup[] GetAllStudentGroups()
	{
		return studentGroupsService.GetAllStudentGroups();
	}

	[HttpGet("student-groups/get_by_id")]
	public StudentGroup? GetStudentGroup([FromQuery] Guid groupId)
	{
		return studentGroupsService.GetStudentGroup(groupId);
	}

	[HttpGet("student-groups/mark_as_deleted")]
	public Result MarkStudentGroupAsDeleted([FromQuery] Guid groupId)
	{
		return studentGroupsService.MarkStudentGroupAsDeleted(groupId);
	}
}
