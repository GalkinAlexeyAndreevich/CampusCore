using CampusCore.Domain.Students;
using CampusCore.Services.Students.Repositories.Models;
using Npgsql;

namespace CampusCore.Services.Students.Repositories.Converters;

internal static class StudentsConverter
{
    internal static Student[] ToStudents(this StudentDb[] studentDbs) => [.. studentDbs.Select(ToStudent)];

    internal static Student ToStudent(this StudentDb studentDb)
    {
        return new Student(
            studentDb.Id,
            studentDb.LastName,
            studentDb.FirstName,
            studentDb.Patronymic,
            studentDb.Gender,
            studentDb.Age,
            studentDb.AverageGrade,
            studentDb.SpecialNotes,
            studentDb.GroupId
        );
    }

    internal static StudentDb ToStudentDb(this NpgsqlDataReader reader)
    {
        return new StudentDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            reader.GetString(reader.GetOrdinal("last_name")),
            reader.GetString(reader.GetOrdinal("first_name")),
            reader.IsDBNull(reader.GetOrdinal("patronymic"))
                ? null
                : reader.GetString(reader.GetOrdinal("patronymic")),
            reader.GetString(reader.GetOrdinal("gender")),
            reader.GetInt32(reader.GetOrdinal("age")),
            reader.GetDecimal(reader.GetOrdinal("average_grade")),
            reader.IsDBNull(reader.GetOrdinal("special_notes"))
                ? null
                : reader.GetFieldValue<String[]>(reader.GetOrdinal("special_notes")),
            reader.GetGuid(reader.GetOrdinal("group_id"))
        );
    }
}