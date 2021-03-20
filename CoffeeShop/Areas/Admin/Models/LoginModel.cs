using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
    }
}