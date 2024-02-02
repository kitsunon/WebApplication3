using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.ViewModels;
using WebApplication3.Model;
using Microsoft.AspNetCore.DataProtection;

namespace WebApplication3.Pages
{
    public class UserDataProtectionService
    {
        private readonly IDataProtector _dataProtector;

        /*public UserDataProtectionService(IDataProtectionProvider dataProtectionProvider)
        {
            // Use the provided data protection provider to create a protector
            _dataProtector = dataProtectionProvider.CreateProtector("MySecretKey");
        }*/

        // Other methods of the UserDataProtectionService...
    }

    public class RegisterModel : PageModel
    {

        private UserManager<AppUser> userManager { get; }
        private SignInManager<AppUser> signInManager { get; }

        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }



        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("MySecretKey");
				var user = new AppUser()
                {
                    UserName = RModel.Email,
                    Email = RModel.Email,
					FirstName = RModel.FirstName,
					LastName = RModel.LastName,
					CreditCard = protector.Protect(RModel.CreditCard),
					MobileNo = RModel.MobileNo,
					BillingAddress = RModel.BillingAddress,
					ShippingAddress = RModel.ShippingAddress
					//Photo = RModel.Photo

				};/*
				if (RModel.Photo != null && RModel.Photo.Length > 0)
				{
					using (var memoryStream = new MemoryStream())
					{
						await RModel.Photo.CopyToAsync(memoryStream);
						user.Photo = memoryStream.ToArray();
					}
				}
				/*
                if (RModel.Photo != null && RModel.Photo.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await RModel.Photo.CopyToAsync(memoryStream);
                        user.Photo = memoryStream.ToArray();
                    }
                }*/
				var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }



    }

}
