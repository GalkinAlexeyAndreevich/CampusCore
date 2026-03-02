using CampusCore.Domain.Products.Enums;

namespace CampusCore.Domain.Products;

public class ProductBlank
{
	public Guid? Id { get; set; }
	public ProductCategory? Category { get; set; }
	public String? Name { get; set; }
	public String? Description { get; set; }
	public Double? Price { get; set; }
}
