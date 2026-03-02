using CampusCore.Domain.Products;
using CampusCore.Domain.Services;
using CampusCore.Services.Products.Repositories.Interfaces;
using CampusCore.Tools.Types.Results;

namespace CampusCore.Services.Products;

public class ProductsService(IProductsRepository productsRepository) : IProductsService
{
	private const Int32 MAX_PRODUCT_NAME_LENGTH = 255;

	public Result SaveProduct(ProductBlank productBlank)
	{
		if (productBlank.Category is null)
			return Result.Failed("Выберите категорию продукта");

		if (!Enum.IsDefined(productBlank.Category.Value))
			return Result.Failed("Выбранная категория не существует. Пожалуйста, выберите другую");

		if (String.IsNullOrWhiteSpace(productBlank.Name))
			return Result.Failed("Введите название продукта");

		if (productBlank.Name.Length == MAX_PRODUCT_NAME_LENGTH)
			return Result.Failed($"Название продукта слишком длинное. Максимально допустимо {MAX_PRODUCT_NAME_LENGTH} символов");

		if (productBlank.Price is null)
			return Result.Failed("Введите стоимость продукта");

		productBlank.Id ??= Guid.NewGuid();

		productsRepository.SaveProduct(productBlank);

		return Result.Success();
	}

	public Page<Product> GetProductsPage(Int32 page, Int32 countInPage)
	{
		return productsRepository.GetProductsPage(page, countInPage);
	}

	public Product? GetProduct(Guid productId)
	{
		return productsRepository.GetProduct(productId);
	}

	public Result MarkProductAsRemoved(Guid productId)
	{
		Product? existProduct = GetProduct(productId);
		if (existProduct is null)
			return Result.Failed("Продукт не найден. Возможно, он был удалён");

		productsRepository.MarkProductAsRemoved(productId);
		return Result.Success();
	}
}
