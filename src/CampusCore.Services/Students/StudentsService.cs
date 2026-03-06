using CampusCore.Domain.Services;
using CampusCore.Domain.StudentGroups;
using CampusCore.Domain.Students;
using CampusCore.Services.StudentGroups.Repositories.Interfaces;
using CampusCore.Services.Students.Repositories.Interfaces;
using CampusCore.Tools.Types.Results;

namespace CampusCore.Services.Students;

public class StudentsService(
    IStudentsRepository studentsRepository,
    IStudentGroupsRepository studentGroupsRepository,
    IStudentNameStatisticsRepository studentNameStatisticsRepository
) : IStudentsService
{
    private const Int32 MAX_NAME_LENGTH = 255;
    private const decimal MIN_AVERAGE_GRADE = 2m;
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

    public StudentScholarship[] CalcScholarshipOnStudents(Guid[] studentIds)
    {
        if (studentIds.Length == 0) return [];

        Student[] students = studentsRepository.GetStudentsByIds(studentIds);
        Guid[] groupIds = students.Select(s => s.GroupId).Distinct().ToArray();
        StudentGroup[] groups = studentGroupsRepository.GetStudentGroupsByIds(groupIds);
        Dictionary<Guid, StudentGroup> groupsById = groups.ToDictionary(g => g.Id, g => g);

        List<StudentScholarship> scholarships = [];

        foreach (Student student in students)
        {
            groupsById.TryGetValue(student.GroupId, out StudentGroup? group);
            if (group is null || student.AverageGrade < MIN_AVERAGE_GRADE_SCHOLARSHIP)
            {
                scholarships.Add(new StudentScholarship(student.Id, 0));
                continue;
            }

            Int32 course = group.CalcCourseSafe();
            Double scholarship = course == 0
                ? 0
                : (Double)(student.AverageGrade * 500) * Math.Sqrt(course);
            scholarships.Add(new StudentScholarship(student.Id, scholarship));
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

    public void InsertStudentNameStatistic()
    {
        StudentNameStatistic[] statistics = CalculateStudentNameStatistics();
        if (statistics.Length == 0) return;

        DateOnly statisticDate = statistics[0].StatisticDate;
        if (studentNameStatisticsRepository.HasForDate(statisticDate)) return;

        foreach (StudentNameStatistic statistic in statistics)
        {
            studentNameStatisticsRepository.SaveStatistic(statistic);
        }
    }

    private StudentNameStatistic[] CalculateStudentNameStatistics()
    {
        Student[] students = studentsRepository.GetAllStudents();
        if (students.Length == 0) return [];

        Guid[] groupIds = students.Select(s => s.GroupId).Distinct().ToArray();
        StudentGroup[] groups = studentGroupsRepository.GetStudentGroupsByIds(groupIds);

        HashSet<Guid> activeGroupIds = groups
            .Where(g => g.CalcCourseSafe() > 0)
            .Select(g => g.Id)
            .ToHashSet();

        DateTime createdAt = DateTime.UtcNow;
        DateOnly statisticDate = DateOnly.FromDateTime(createdAt);

        return students
            .Where(s => activeGroupIds.Contains(s.GroupId))
            .Select(s => s.FirstName?.Trim())
            .Where(name => !String.IsNullOrWhiteSpace(name))
            .GroupBy(name => name!, StringComparer.OrdinalIgnoreCase)
            .Select(g => new StudentNameStatistic(statisticDate, g.Key, g.Count(), createdAt))
            .ToArray();
    }
}