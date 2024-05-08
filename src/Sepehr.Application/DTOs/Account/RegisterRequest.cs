using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.Account
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "نام الزامی می باشد !")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage ="نام خانوادگی الزامی می باشد !")]
        public required string LastName { get; set; }

        //[Required]
        [EmailAddress(ErrorMessage ="آدرس ایمیل نامعتبر می باشد !")]
        public required string Email { get; set; }
        [Required]
        [MinLength(6,ErrorMessage ="نام کاربری نمی تواند کمتر از 6 کاراکتر باشد !")]
        public required string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "کلمه عبور نمی تواند کمتر از 6 کاراکتر باشد !")]
        public required string Password { get; set; }

        [Required]
        [Compare("Password",ErrorMessage ="کلمه عبور و تکرار آن یکسان نیستند !")]
        public required string ConfirmPassword { get; set; }
    }
}
