namespace CampusCore.Services.Students.Repositories.Queries;

internal static class Sql
{
    internal static String StudentSave =>
        """
            INSERT INTO students (
                id,
                last_name,
                first_name,
                patronymic,
                gender,
                age,
                average_grade,
                special_notes,
                group_id
            )
            VALUES (
                @p_id,
                @p_last_name,
                @p_first_name,
                @p_patronymic,
                @p_gender,
                @p_age,
                @p_average_grade,
                @p_special_notes,
                @p_group_id
            )
        ON CONFLICT (id) DO UPDATE SET
        last_name = @p_last_name,
        first_name = @p_first_name,
        patronymic = @p_patronymic,
        gender = @p_gender,
        age = @p_age,
        average_grade = @p_average_grade,
        special_notes = @p_special_notes,
        group_id = @p_group_id
        """;

    internal static String GetAllStudents =>
        """
        	SELECT * FROM students 
        	WHERE  deleted_at IS NULL
        	ORDER BY created_at DESC 
        """;

    internal static String GetStudentById => "SELECT * FROM students WHERE id = @p_studentId AND deleted_at IS NULL";

    internal static String MarkStudentAsDeleted =>
        """
        	UPDATE students
        	SET deleted_at = @p_deletedAt
        	WHERE id = @p_studentId
        """;
}