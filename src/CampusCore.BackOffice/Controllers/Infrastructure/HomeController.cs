using Microsoft.AspNetCore.Mvc;

namespace CampusCore.BackOffice.Controllers.Infrastructure;

public class HomeController : AppController
{
	[Route("/"), Route("/products")]
	public IActionResult Index() => App();
}
