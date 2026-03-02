using CampusCore.Domain.Products;
using CampusCore.Domain.Services;
using CampusCore.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace CampusCore.BackOffice.Controllers.StudentGroups;

public class StudentsController(IProductsService productsService) : AppController
{
	[HttpPost("student-group/save")]
	public Result SaveProducts([FromBody] ProductBlank productBlank)
	{
		return productsService.SaveProduct(productBlank);
	}

	[HttpGet("student-group/get_page")]
	public Page<Product> GetStudentsPage([FromQuery] Int32 page, [FromQuery] Int32 countInPage)
	{
		return productsService.GetProductsPage(page, countInPage);
	}

	[HttpGet("student-group/get_by_id")]
	public Product? GetStudent([FromQuery] Guid productId)
	{
		return productsService.GetProduct(productId);
	}

	[HttpGet("student-group/mark_student_as_removed")]
	public Result MarkStudentAsRemoved([FromQuery] Guid productId)
	{
		return productsService.MarkProductAsRemoved(productId);
	}
}
