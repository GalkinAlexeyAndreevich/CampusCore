using Microsoft.AspNetCore.Mvc;

namespace CampusCore.BackOffice.Controllers.Infrastructure;

public class HomeController : AppController
{
	[Route("/"), Route("/products"), Route("/student_group")]
	public IActionResult Index() => App();
}
