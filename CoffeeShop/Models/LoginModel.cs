using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Hãy nhập email")]
        public string Email { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Hãy nhập mật khẩu")]
        public string Password { get; set; }
    }
}