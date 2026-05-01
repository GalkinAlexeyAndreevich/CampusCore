using CampusCore.BackOffice.Controllers.Students.Requests;
using CampusCore.Domain.Students;
using CampusCore.Domain.Services;
using CampusCore.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace CampusCore.BackOffice.Controllers.Students;

[Route("api")]
public class StudentsController(IStudentsService studentsService) : AppController
{
	[HttpPost("students/save")]
	public Result SaveStudents([FromBody] StudentBlank studentBlank)
	{
		return studentsService.SaveStudent(studentBlank);
	}

	[HttpGet("students")]
	public Student[] GetAllStudents()
	{
		return studentsService.GetAllStudents();
	}
	
	[HttpGet("students/detail")]
	public StudentDetail[] GetAllStudentsDetailed()
	{
		return studentsService.GetAllStudentsDetailed();
	}

	[HttpGet("students/get_by_id")]
	public Student? GetStudent([FromQuery] Guid studentId)
	{
		return studentsService.GetStudent(studentId);
	}
	
	[HttpPost("students/get_student_count_on_groups")]
	public StudentCountOnGroup[] GetStudentCountOnGroups([FromBody] GetStudentCountOnGroupsRequest request)
	{
		Guid[] ids = request.GroupIds ?? [];
		return studentsService.GetStudentsCountOnGroupIds(ids);
	}
	
	[HttpPost("students/calc-scholarships")]
	public StudentScholarship[]? CalcScholarshipOnStudents([FromBody] CalcScholarshipRequest request)
	{
		Guid[] ids = request.StudentIds ?? [];
		return studentsService.CalcScholarshipOnStudents(ids);
	}
	
	[HttpPost("students/mark_as_deleted")]
	public Result MarkStudentAsDeleted([FromQuery] Guid studentId)
	{
		return studentsService.MarkStudentAsDeleted(studentId);
	}
}
