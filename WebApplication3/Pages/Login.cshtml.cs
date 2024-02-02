using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Model;
using WebApplication3.ViewModels;
using Newtonsoft.Json;

namespace WebApplication3.Pages
{
	public class LoginModel : PageModel
	{
		[BindProperty]
		public Login LModel { get; set; }
		private readonly SignInManager<AppUser> signInManager;
		public LoginModel(SignInManager<AppUser> signInManager)
		{
			this.signInManager = signInManager;
		}
		public void OnGet()
		{
		}
		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{/*
				var recaptchaResponse = HttpContext.Request.Form["g-recaptcha-response"];
				var isCaptchaValid = await VerifyRecaptcha(recaptchaResponse);
				if (!isCaptchaValid)
				{
					ModelState.AddModelError("", "reCAPTCHA validation failed.");
					return Page();
				}*/
				var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, LModel.RememberMe, lockoutOnFailure: true);
				if (identityResult.Succeeded)
				{
					return RedirectToPage("Index");
				}
                else if (identityResult.IsLockedOut)
                {
                    // Handle the case where the user is locked out
                    // You can display a message or redirect the user to a lockout page
                    ModelState.AddModelError("", "Account locked out. Please try again later.");
                }
                else
                {
                    // Handle other cases of failed login (e.g., invalid credentials)
                    // You can display a message or take appropriate action
                    ModelState.AddModelError("", "Username or Password incorrect");
                }
                ModelState.AddModelError("", "Username or Password incorrect");
			}
			return Page();
		}
		private async Task<bool> VerifyRecaptcha(string recaptchaResponse)
		{
			// Send a request to Google's reCAPTCHA verification endpoint
			// using your secret key and the user's response
			// You may want to use HttpClient to make this request

			// Example:
			var secretKey = "YOUR_SECRET_KEY";
			var verificationUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={recaptchaResponse}";

			// Use HttpClient to send a request to the verification endpoint
			using (var httpClient = new HttpClient())
			{
				var response = await httpClient.GetStringAsync(verificationUrl);
				var result = JsonConvert.DeserializeObject<RecaptchaVerificationResult>(response);

				return result.Success;
			}
		}
		public class RecaptchaVerificationResult
		{
			public bool Success { get; set; }
			public string ChallengeTs { get; set; }
			public string Hostname { get; set; }
			// You may include other properties from the reCAPTCHA response if needed
		}
	}
}
