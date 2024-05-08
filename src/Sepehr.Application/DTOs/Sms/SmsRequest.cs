using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.Sms
{
    public class SmsRequest
    {
        public required string Mobile { get; set; }
        public required string Message { get; set; }
    }
}
