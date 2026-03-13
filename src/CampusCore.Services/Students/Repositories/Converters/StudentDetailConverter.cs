using CampusCore.Domain.StudentGroups;
using CampusCore.Domain.StudentGroups.Enums;
using CampusCore.Domain.Students;
using Npgsql;

namespace CampusCore.Services.Students.Repositories.Converters;

internal static class StudentDetailConverter
{
    internal static StudentDetail ToStudentDetail(this NpgsqlDataReader reader)
    {
        Student student = reader.ToStudentDb().ToStudent();

        StudentGroup? group = null;

        if (student.GroupId != Guid.Empty)
        {
            group = new StudentGroup(
                student.GroupId,
                reader.GetString(reader.GetOrdinal("group_name")),
                reader.GetString(reader.GetOrdinal("group_abbreviation")),
                (TrainingFormat)reader.GetInt32(reader.GetOrdinal("group_training_format")),
                reader.GetInt32(reader.GetOrdinal("group_study_start_year")),
                reader.GetInt32(reader.GetOrdinal("group_study_end_year"))
            );
        }

        return new StudentDetail(
            student.Id,
            student.FirstName,
            student.LastName,
            student.Patronymic,
            student.Gender,
            student.DateOfBirth,
            student.AverageGrade,
            student.SpecialNotes,
            student.GroupId,
            group
        );
    }
}