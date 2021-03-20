using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class RegisterModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Hãy nhập email của bạn!")]
        public string Email { get; set; }
        [Display(Name = "Tên của bạn")]
        [Required(ErrorMessage = "Hãy nhập tên của bạn!")]
        public string CustomerName { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Hãy nhập password!")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 kí tự")]
        public string Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateTime? DOB { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
    }
}