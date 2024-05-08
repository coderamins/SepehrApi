using Sepehr.Application.DTOs.Captcha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.Account
{
    public class AuthenticationRequest: CaptchaDto
    {
        //public required string Email { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
