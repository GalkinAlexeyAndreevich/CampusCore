using CampusCore.Domain.Products;
using CampusCore.Tools.Types.Results;

namespace CampusCore.Domain.Services;

public interface IProductsService
{
	Result SaveProduct(ProductBlank productBlank);
	Page<Product> GetProductsPage(Int32 page, Int32 countInPage);
	Product? GetProduct(Guid productId);
	Result MarkProductAsRemoved(Guid productId);
}
