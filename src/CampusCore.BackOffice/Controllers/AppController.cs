using Microsoft.AspNetCore.Mvc;

namespace CampusCore.BackOffice.Controllers;

public class AppController : Controller
{
	public IActionResult App()
	{
		return View("App");
	}
}
