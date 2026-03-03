using CampusCore.Domain.StudentGroups;
using CampusCore.Services.StudentGroups.Repositories.Converters;
using CampusCore.Services.StudentGroups.Repositories.Interfaces;
using CampusCore.Services.StudentGroups.Repositories.Models;
using CampusCore.Services.StudentGroups.Repositories.Queries;
using CampusCore.Tools.Utils;

namespace CampusCore.Services.StudentGroups.Repositories;

public class StudentGroupsRepository : IStudentGroupsRepository
{
	public void SaveStudentGroup(StudentGroupBlank studentGroupBlank)
	{
		DatabaseUtils.Execute(
			Sql.StudentGroupSave,
			(parameters) =>
			{
				parameters.AddWithValue("p_id", studentGroupBlank.Id!.Value);
				parameters.AddWithValue("p_name", studentGroupBlank.Name!);
				parameters.AddWithValue("p_abbreviation", studentGroupBlank.Abbreviation!);
				parameters.AddWithValue("p_training_format", (int)studentGroupBlank.TrainingFormat!);
				parameters.AddWithValue("p_study_start_year", studentGroupBlank.StudyStartYear!.Value);
				parameters.AddWithValue("p_study_end_year", studentGroupBlank.StudyEndYear!.Value);
				parameters.AddWithValue("p_date_now", DateTime.UtcNow);
			}
		);
	}

	public StudentGroup[] GetAllStudentGroups()
	{
		StudentGroupDb[] groupDbs = DatabaseUtils.GetAll(
			Sql.GetAllStudentGroups,
			(parameters) => { },
			(reader) => reader.ToStudentGroupDb()
		);

		return groupDbs.ToStudentGroups();
	}

	public StudentGroup? GetStudentGroup(Guid groupId)
	{
		return DatabaseUtils
			.Get<StudentGroupDb?>(
				Sql.GetStudentGroupById,
				(parameters) =>
				{
					parameters.AddWithValue("@p_groupId", groupId);
				},
				(reader) => reader.ToStudentGroupDb()
			)
			?.ToStudentGroup();
	}

	public void MarkStudentGroupAsDeleted(Guid groupId)
	{
		DatabaseUtils.Execute(
			Sql.MarkStudentGroupAsDeleted,
			(parameters) =>
			{
				parameters.AddWithValue("p_groupId", groupId);
				parameters.AddWithValue("p_deletedAt", DateTime.UtcNow);
			}
		);
	}
}

