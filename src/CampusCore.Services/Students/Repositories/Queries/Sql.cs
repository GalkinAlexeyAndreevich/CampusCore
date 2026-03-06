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
                date_of_birth,
                average_grade,
                special_notes,
                group_id,
                created_at
            )
            VALUES (
                @p_id,
                @p_last_name,
                @p_first_name,
                @p_patronymic,
                @p_gender,
                @p_date_of_birth,
                @p_average_grade,
                @p_special_notes,
                @p_group_id,
                @p_date_now
            )
        ON CONFLICT (id) DO UPDATE SET
        last_name = @p_last_name,
        first_name = @p_first_name,
        patronymic = @p_patronymic,
        gender = @p_gender,
        date_of_birth = @p_date_of_birth,
        average_grade = @p_average_grade,
        special_notes = @p_special_notes,
        group_id = @p_group_id,
        updated_at = @p_date_now
        """;
    
    internal static String GetAllStudents =>
        """
        	SELECT * FROM students 
        	WHERE  deleted_at IS NULL
        	ORDER BY created_at DESC 
        """;

    internal static String GetStudentById => "SELECT * FROM students WHERE id = @p_studentId AND deleted_at IS NULL";
    internal static String GetStudentsByIds => "SELECT * FROM students WHERE id = ANY(@p_studentIds) AND deleted_at IS NULL";

    internal static String MarkStudentAsDeleted =>
        """
        	UPDATE students
        	SET deleted_at = @p_deletedAt
        	WHERE id = @p_studentId
        """;

    internal static String SaveStudentNameStatistic =>
        """
            INSERT INTO student_name_statistic (
                id,
                statistic_date,
                name,
                repeat_count,
                created_at
            )
            VALUES (
                @p_id,
                @p_statistic_date,
                @p_name,
                @p_repeat_count,
                @p_created_at
            )
            ON CONFLICT (statistic_date, name) DO UPDATE SET
                repeat_count = EXCLUDED.repeat_count
        """;

    internal static String HasStudentNameStatisticByDate =>
        """
            SELECT 1
            FROM student_name_statistic
            WHERE statistic_date = @p_statistic_date
            LIMIT 1
        """;

    internal static String GetStudentNameStatistics =>
        """
            SELECT * FROM student_name_statistic
            ORDER BY statistic_date DESC, repeat_count DESC
        """;
}