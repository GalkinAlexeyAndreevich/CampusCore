using CampusCore.Domain.Services;
using CampusCore.Services.Students;
using CampusCore.Services.Students.Repositories;
using CampusCore.Services.Students.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CampusCore.Services;

public static class ServiceConfigurator
{
	public static IServiceCollection AddServices(this IServiceCollection collection)
	{
		collection.AddSingleton<IStudentsService, StudentsService>();

		collection.AddSingleton<IStudentsRepository, StudentsRepository>();

		return collection;
	}
}
