using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace WebApplication3.Controllers
{
	public class UserController : Controller
	{
		[Route("api/[controller]")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
