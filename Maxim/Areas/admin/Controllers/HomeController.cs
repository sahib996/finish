using Microsoft.AspNetCore.Mvc;

namespace Maxim.Areas.admin.Controllers
{
	public class HomeController : Controller
	{
		[Area("admin")]
		/*[Authorize]*/
		public async Task<IActionResult> Index()
		{
			return View();
		}
	}
}
