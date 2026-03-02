using CampusCore.Domain.Students;
using CampusCore.Services.Students.Repositories.Converters;
using CampusCore.Services.Students.Repositories.Interfaces;
using CampusCore.Services.Students.Repositories.Models;
using CampusCore.Services.Students.Repositories.Queries;
using CampusCore.Tools.Utils;

namespace CampusCore.Services.Students.Repositories;

public class StudentsRepository : IStudentsRepository
{
	public void SaveStudent(StudentBlank studentBlank)
	{
		DatabaseUtils.Execute(
			Sql.StudentSave,
			(parameters) =>
			{
				parameters.AddWithValue("p_id", studentBlank.Id!.Value);
				parameters.AddWithValue("p_last_name", studentBlank.LastName!);
				parameters.AddWithValue("p_first_name", studentBlank.FirstName!);
				parameters.AddWithValue("p_patronymic", (Object?)studentBlank.Patronymic ?? DBNull.Value);
				parameters.AddWithValue("p_gender", studentBlank.Gender!);
				parameters.AddWithValue("p_age", studentBlank.Age!);
				parameters.AddWithValue("p_average_grade", studentBlank.AverageGrade!);
				parameters.AddWithValue("p_special_notes", (Object?)studentBlank.SpecialNotes ?? DBNull.Value);
				parameters.AddWithValue("p_group_id", studentBlank.GroupId!.Value);
				parameters.AddWithValue("p_date_now", DateTime.UtcNow);
			}
		);
	}

	public Student[] GetAllStudents()
	{
		StudentDb[] studentDbs = DatabaseUtils.GetAll(
			Sql.GetAllStudents,
			(parameters) => { },
			(reader) => reader.ToStudentDb()
		);

		return studentDbs.ToStudents();
	}

	public Student? GetStudent(Guid studentId)
	{
		return DatabaseUtils
			.Get<StudentDb?>(
				Sql.GetStudentById,
				(parameters) =>
				{
					parameters.AddWithValue("@p_studentId", studentId);
				},
				(reader) => reader.ToStudentDb()
			)
			?.ToStudent();
	}

	public void MarkStudentAsDeleted(Guid studentId)
	{
		DatabaseUtils.Execute(
			Sql.MarkStudentAsDeleted,
			(parameters) =>
			{
				parameters.AddWithValue("p_studentId", studentId);
				parameters.AddWithValue("p_deletedAt", DateTime.UtcNow);
			}
		);
	}
}
