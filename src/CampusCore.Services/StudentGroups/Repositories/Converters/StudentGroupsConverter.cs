using CampusCore.Domain.StudentGroups;
using CampusCore.Services.StudentGroups.Repositories.Models;
using Npgsql;

namespace CampusCore.Services.StudentGroups.Repositories.Converters;

internal static class StudentGroupsConverter
{
	internal static StudentGroup[] ToStudentGroups(this StudentGroupDb[] groupDbs) => [.. groupDbs.Select(ToStudentGroup)];

	internal static StudentGroup ToStudentGroup(this StudentGroupDb groupDb)
	{
		return new StudentGroup(
			groupDb.Id,
			groupDb.Name,
			groupDb.Abbreviation,
			groupDb.TrainingFormat,
			groupDb.StudyStartYear,
			groupDb.StudyEndYear
		);
	}

	internal static StudentGroupDb ToStudentGroupDb(this NpgsqlDataReader reader)
	{
		return new StudentGroupDb(
			reader.GetGuid(reader.GetOrdinal("id")),
			reader.GetString(reader.GetOrdinal("name")),
			reader.GetString(reader.GetOrdinal("abbreviation")),
			reader.GetString(reader.GetOrdinal("training_format")),
			reader.GetInt32(reader.GetOrdinal("study_start_year")),
			reader.GetInt32(reader.GetOrdinal("study_end_year"))
		);
	}
}

