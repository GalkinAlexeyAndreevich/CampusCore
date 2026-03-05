using Microsoft.AspNetCore.Mvc;

namespace CampusCore.BackOffice.Controllers.Infrastructure;

public class HomeController : AppController
{
	[Route("/")]
	public IActionResult Index() => App();
}
