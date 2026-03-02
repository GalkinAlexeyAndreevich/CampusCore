using CampusCore.Domain.Services;
using CampusCore.Domain.Students;
using CampusCore.Services.Students.Repositories.Interfaces;
using CampusCore.Tools.Types.Results;

namespace CampusCore.Services.Students;

public class StudentsService(IStudentsRepository studentsRepository) : IStudentsService
{
    private const Int32 MAX_NAME_LENGTH = 255;
    private const Int32 MAX_GENDER_LENGTH = 32;
    private const decimal MIN_AVERAGE_GRADE = 0m;
    private const decimal MAX_AVERAGE_GRADE = 5m;

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
            return Result.Failed($"Отчество студента слишком длинное. Максимально допустимо {MAX_NAME_LENGTH} символов");

        if (String.IsNullOrWhiteSpace(studentBlank.Gender))
            return Result.Failed("Укажите пол студента");

        if (studentBlank.Gender.Length > MAX_GENDER_LENGTH)
            return Result.Failed($"Пол студента указан слишком длинно. Максимально допустимо {MAX_GENDER_LENGTH} символов");

        if (studentBlank.Age is null)
            return Result.Failed("Укажите возраст студента");

        if (studentBlank.Age.Value <= 0 || studentBlank.Age.Value > 200)
            return Result.Failed("Возраст студента должен быть в диапазоне от 0 до 200");

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

    public Result MarkStudentAsDeleted(Guid studentId)
    {
        Student? existStudent = GetStudent(studentId);
        if (existStudent is null)
            return Result.Failed("Студент не найден. Возможно, он был удалён");

        studentsRepository.MarkStudentAsDeleted(studentId);
        return Result.Success();
    }
}
