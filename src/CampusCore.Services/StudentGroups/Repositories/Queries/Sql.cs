namespace CampusCore.Services.StudentGroups.Repositories.Queries;

internal static class Sql
{
	internal static String StudentGroupSave =>
		"""
			INSERT INTO student_groups (
				id,
				name,
				abbreviation,
				training_format,
				study_start_year,
				study_end_year,
				created_at
			)
			VALUES (
				@p_id,
				@p_name,
				@p_abbreviation,
				@p_training_format,
				@p_study_start_year,
				@p_study_end_year,
				@p_date_now
			)
			ON CONFLICT (id) DO UPDATE SET
				name = @p_name,
				abbreviation = @p_abbreviation,
				training_format = @p_training_format,
				study_start_year = @p_study_start_year,
				study_end_year = @p_study_end_year,
				updated_at = @p_date_now
		""";

	internal static String GetAllStudentGroups =>
		"""
			SELECT * FROM student_groups
			WHERE deleted_at IS NULL
			ORDER BY created_at DESC
		""";

	internal static String GetStudentGroupById =>
		"""
			SELECT * FROM student_groups
			WHERE id = @p_groupId AND deleted_at IS NULL
		""";

	internal static String MarkStudentGroupAsDeleted =>
		"""
			UPDATE student_groups
			SET deleted_at = @p_deletedAt
			WHERE id = @p_groupId
		""";
}

