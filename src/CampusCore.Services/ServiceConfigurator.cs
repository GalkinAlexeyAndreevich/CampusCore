using CampusCore.Domain.Services;
using CampusCore.Services.Products;
using CampusCore.Services.Products.Repositories;
using CampusCore.Services.Products.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CampusCore.Services;

public static class ServiceConfigurator
{
	public static IServiceCollection AddServices(this IServiceCollection collection)
	{
		collection.AddSingleton<IProductsService, ProductsService>();

		collection.AddSingleton<IProductsRepository, ProductsRepository>();

		return collection;
	}
}
