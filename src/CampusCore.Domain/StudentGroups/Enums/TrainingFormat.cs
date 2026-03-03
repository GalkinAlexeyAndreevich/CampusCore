using System.ComponentModel.DataAnnotations;

namespace CampusCore.Domain.StudentGroups.Enums;

public enum TrainingFormat
{
	[Display(Name = "Очный")]
	FullTime = 1,

	[Display(Name = "Заочный")]
	PartTime = 2,
}
