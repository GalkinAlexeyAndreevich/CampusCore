using System.Collections.Generic;
using System.Linq;
using CampusCore.Domain.Services;
using CampusCore.Domain.StudentGroups;
using CampusCore.Domain.Students;
using CampusCore.Services.StudentGroups.Repositories.Interfaces;
using CampusCore.Services.Students.Repositories.Interfaces;
using CampusCore.Tools.Types.Results;

namespace CampusCore.Services.Students;

public class StudentsService(
    IStudentsRepository studentsRepository,
    IStudentGroupsRepository studentGroupsRepository
) : IStudentsService
{
    private const Int32 MAX_NAME_LENGTH = 255;
    private const decimal MIN_AVERAGE_GRADE = 0m;
    private const decimal MAX_AVERAGE_GRADE = 5m;
    private const decimal MIN_AVERAGE_GRADE_SCHOLARSHIP = 4m;

    public Result SaveStudent(StudentBlank studentBlank)
    {
        if (String.IsNullOrWhiteSpace(studentBlank.LastName))
            return Result.Failed("Введите фамилию студента");

        if (studentBlank.LastName.Length > MAX_NAME_LENGTH)
            return Result.Failed($"Фамилия студента слишком длинная. Максимально допустимо {MAX_NAME_LENGTH} символов");

        if (String.IsNullOrWhiteSpace(studentBlank.FirstName))
            return Result.Failed("Введите имя студента");

        if (studentBlank.FirstName.Length > MAX_NAME_LENGTH)
            return Result.Failed($"Имя студента слишком длинное. Максимально допустимо {MAX_NAME_LENGTH} символов");

        if (!String.IsNullOrWhiteSpace(studentBlank.Patronymic) && studentBlank.Patronymic.Length > MAX_NAME_LENGTH)
            return Result.Failed(
                $"Отчество студента слишком длинное. Максимально допустимо {MAX_NAME_LENGTH} символов");

        if (studentBlank.Gender is null)
            return Result.Failed("Укажите пол студента");

        if (!Enum.IsDefined(studentBlank.Gender.Value))
            return Result.Failed("Пол может быть либо мужской, либо женский");

        if (studentBlank.DateOfBirth is null)
            return Result.Failed("Укажите дату рождения студента");

        if (studentBlank.DateOfBirth.Value.Date > DateTime.Today)
            return Result.Failed("Дата рождения не может быть в будущем");

        if (studentBlank.DateOfBirth.Value.Date < DateTime.MinValue)
            return Result.Failed("Дата рождения слишком старая");

        if (studentBlank.AverageGrade is null)
            return Result.Failed("Укажите средний балл студента");

        if (studentBlank.AverageGrade.Value < MIN_AVERAGE_GRADE || studentBlank.AverageGrade.Value > MAX_AVERAGE_GRADE)
            return Result.Failed($"Средний балл должен быть в диапазоне от {MIN_AVERAGE_GRADE} до {MAX_AVERAGE_GRADE}");

        if (studentBlank.GroupId is null || studentBlank.GroupId.Value == Guid.Empty)
            return Result.Failed("Выберите учебную группу");

        if (studentBlank.SpecialNotes is not null && studentBlank.SpecialNotes.Any(String.IsNullOrWhiteSpace))
            return Result.Failed("Заметки не должны содержать пустые значения");

        studentBlank.Id ??= Guid.NewGuid();

        studentsRepository.SaveStudent(studentBlank);

        return Result.Success();
    }

    public Student[] GetAllStudents()
    {
        return studentsRepository.GetAllStudents();
    }

    public Student? GetStudent(Guid studentId)
    {
        return studentsRepository.GetStudent(studentId);
    }

    private static Int32 CalcCourseSafe(StudentGroup group, DateTime now)
    {
        try
        {
            Int32 academicYearStartYear = now.Month >= 9 ? now.Year : now.Year - 1;

            if (academicYearStartYear < group.StudyStartYear) return 0;

            Int32 course = academicYearStartYear - group.StudyStartYear + 1;
            Int32 lastCourse = Math.Max(1, group.StudyEndYear - group.StudyStartYear);

            if (course < 1) return 0;
            if (course > lastCourse) return 0;

            return course;
        }
        catch
        {
            return 0;
        }
    }

    public StudentScholarship[]? CalcScholarshipOnStudents(Guid[] studentIds)
    {
        Console.WriteLine($"studentIds {studentIds}");
        Student[] students = studentsRepository.GetStudentsByIds(studentIds);
        Console.WriteLine($"students length {students.Length}");
        Guid[] groupIds = students.Select(s => s.GroupId).ToArray();
        StudentGroup[] groups = studentGroupsRepository.GetStudentGroupsByIds(groupIds);

        List<StudentScholarship> scholarships = [];
        DateTime now = DateTime.Now;

        foreach (Student student in students)
        {
            StudentGroup? group = groups.FirstOrDefault(g => g.Id == student.GroupId);
            Console.WriteLine($"{student.Id} {group?.Name}");
            if (group is null || student.AverageGrade < MIN_AVERAGE_GRADE_SCHOLARSHIP)
            {
                scholarships.Add(new StudentScholarship(student.Id, 0));
                continue;
            }

            Int32 course = CalcCourseSafe(group, now);

            Double scholarship = course == 0
                ? 0
                : (Double)(student.AverageGrade * 500) * Math.Sqrt(course);

            StudentScholarship scholarshipResult = new StudentScholarship(student.Id, scholarship);
            Console.WriteLine($"{student.Id} {scholarship} {scholarshipResult.StudentId} {scholarshipResult.Scholarship}");
            scholarships.Add(scholarshipResult);
        }

        return scholarships.ToArray();
    }

    public Result MarkStudentAsDeleted(Guid studentId)
    {
        Student? existStudent = GetStudent(studentId);
        if (existStudent is null)
            return Result.Failed("Студент не найден. Возможно, он был удалён");

        studentsRepository.MarkStudentAsDeleted(studentId);
        return Result.Success();
    }
}