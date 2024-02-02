using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Model;

namespace WebApplication3.Pages
{
    public class LogoutModel : PageModel
    {
		private readonly SignInManager<AppUser> signInManager;
		public LogoutModel(SignInManager<AppUser> signInManager)
		{
			this.signInManager = signInManager;
		}
			public void OnGet()
        {
        }
		public async Task<IActionResult> OnPostLogoutAsync()
		{

			await signInManager.SignOutAsync();
			return RedirectToPage("Register");
		}

		public async Task<IActionResult> OnPostDontLogoutAsync()
		{
			return RedirectToPage("Index");
		}
	}
}
