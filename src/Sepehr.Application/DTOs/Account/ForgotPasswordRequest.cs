using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.Account
{
    public class ForgotPasswordRequest
    {
        [Required]
        public required string UserName { get; set; }
    }
}
