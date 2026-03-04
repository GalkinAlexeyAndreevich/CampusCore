using CampusCore.Domain.Students;
using CampusCore.Domain.Services;
using CampusCore.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace CampusCore.BackOffice.Controllers.Students;

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

	[HttpGet("students/get_by_id")]
	public Student? GetStudent([FromQuery] Guid studentId)
	{
		return studentsService.GetStudent(studentId);
	}
	
	public class CalcScholarshipRequest
	{
		public Guid[]? StudentIds { get; set; }
	}
	
	[HttpPost("students/calc-scholarships")]
	public StudentScholarship[]? CalcScholarshipOnStudents([FromBody] CalcScholarshipRequest request)
	{
		Guid[] ids = request.StudentIds ?? Array.Empty<Guid>();
		Console.WriteLine($"calc-scholarships studentIdsCount={ids.Length}");
		return studentsService.CalcScholarshipOnStudents(ids);
	}
	

	[HttpPost("students/mark_as_deleted")]
	public Result MarkStudentAsDeleted([FromQuery] Guid studentId)
	{
		return studentsService.MarkStudentAsDeleted(studentId);
	}
}
