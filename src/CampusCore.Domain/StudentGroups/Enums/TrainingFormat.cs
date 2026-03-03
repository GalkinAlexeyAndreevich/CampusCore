using System.ComponentModel.DataAnnotations;

namespace CampusCore.Domain.StudentGroups.Enums;

public enum TrainingFormat
{
	[Display(Name = "Очная")]
	FullTime = 1,

	[Display(Name = "Заочная")]
	PartTime = 2,
}
