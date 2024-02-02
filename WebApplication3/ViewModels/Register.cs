using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace WebApplication3.ViewModels
{
    public class Register
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

		[Required]
		[MinLength(12, ErrorMessage = "Enter at least a 12 characters password")]
		//[DataType(DataType.Password)]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}$", ErrorMessage = "Passwords must contain at least an upper case letter, lower case letter, digit and a symbol")]
         public string Password { get; set; }


		[Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must contain only letters.")]
        public string FirstName { get; set; }


        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name must contain only letters.")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Credit card must contain only numeric digits.")]
        public string CreditCard { get; set; }


        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Mobile Number must contain only numeric digits.")]
        public string MobileNo { get; set; }
        [Required]
        public string BillingAddress { get; set; }

		//[Required]

		//public byte[] Photo { get; set; }
		//[Required(ErrorMessage = "Please select a photo.")]
		//public IFormFile Photo { get; set; }


        [Required]
        public string ShippingAddress { get; set; }
    }
}
