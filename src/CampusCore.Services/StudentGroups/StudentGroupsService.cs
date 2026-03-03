using CampusCore.Domain.Services;
using CampusCore.Domain.StudentGroups;
using CampusCore.Services.StudentGroups.Repositories.Interfaces;
using CampusCore.Tools.Types.Results;

namespace CampusCore.Services.StudentGroups;

public class StudentGroupsService(IStudentGroupsRepository studentGroupsRepository) : IStudentGroupsService
{
	private const Int32 MAX_NAME_LENGTH = 255;
	private const Int32 MAX_ABBREVIATION_LENGTH = 64;
	private const Int32 MIN_YEAR = 1900;
	private const Int32 MAX_YEAR = 2100;

	public Result SaveStudentGroup(StudentGroupBlank studentGroupBlank)
	{
		if (String.IsNullOrWhiteSpace(studentGroupBlank.Name))
			return Result.Failed("Введите название группы");

		if (studentGroupBlank.Name.Length > MAX_NAME_LENGTH)
			return Result.Failed($"Название группы слишком длинное. Максимально допустимо {MAX_NAME_LENGTH} символов");

		if (String.IsNullOrWhiteSpace(studentGroupBlank.Abbreviation))
			return Result.Failed("Введите аббревиатуру группы");

		if (studentGroupBlank.Abbreviation.Length > MAX_ABBREVIATION_LENGTH)
			return Result.Failed($"Аббревиатура группы слишком длинная. Максимально допустимо {MAX_ABBREVIATION_LENGTH} символов");

		if (studentGroupBlank.TrainingFormat is null)
			return Result.Failed("Укажите формат обучения");

		if (!Enum.IsDefined(studentGroupBlank.TrainingFormat.Value))
			return Result.Failed("Формат обучения может быть очный или заочный");

		if (studentGroupBlank.StudyStartYear is null)
			return Result.Failed("Укажите год начала обучения");

		if (studentGroupBlank.StudyEndYear is null)
			return Result.Failed("Укажите год окончания обучения");

		if (studentGroupBlank.StudyStartYear.Value < MIN_YEAR || studentGroupBlank.StudyStartYear.Value > MAX_YEAR)
			return Result.Failed($"Год начала обучения должен быть в диапазоне от {MIN_YEAR} до {MAX_YEAR}");

		if (studentGroupBlank.StudyEndYear.Value < MIN_YEAR || studentGroupBlank.StudyEndYear.Value > MAX_YEAR)
			return Result.Failed($"Год окончания обучения должен быть в диапазоне от {MIN_YEAR} до {MAX_YEAR}");

		if (studentGroupBlank.StudyStartYear.Value > studentGroupBlank.StudyEndYear.Value)
			return Result.Failed("Год начала обучения не может быть больше года окончания обучения");

		studentGroupBlank.Id ??= Guid.NewGuid();

		studentGroupsRepository.SaveStudentGroup(studentGroupBlank);

		return Result.Success();
	}

	public StudentGroup[] GetAllStudentGroups()
	{
		return studentGroupsRepository.GetAllStudentGroups();
	}

	public StudentGroup? GetStudentGroup(Guid groupId)
	{
		return studentGroupsRepository.GetStudentGroup(groupId);
	}

	public Result MarkStudentGroupAsDeleted(Guid groupId)
	{
		StudentGroup? existGroup = GetStudentGroup(groupId);
		if (existGroup is null)
			return Result.Failed("Группа не найдена. Возможно, она была удалена");

		studentGroupsRepository.MarkStudentGroupAsDeleted(groupId);
		return Result.Success();
	}
}

