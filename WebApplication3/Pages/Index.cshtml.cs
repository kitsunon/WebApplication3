using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebApplication3.Model;

namespace WebApplication3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public IndexModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public AppUser CurrentUser { get; set; }

        public void OnGet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("MySecretKey");
                CurrentUser = _userManager.FindByIdAsync(userId).Result;

                if (CurrentUser != null)
                {

                    CurrentUser.CreditCard = protector.Unprotect(CurrentUser.CreditCard);
                }
                else 
                {
                    
                }
            }
            else
            {

            }
        }
        protected void LogoutMe(object sender, EventArgs eventArgs)
        {
            
        }
    }
}