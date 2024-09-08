using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.Sms
{
    public class SmsRequest
    {
        public required IEnumerable<string> mobiles { get; set; }
        public required IEnumerable<string> messageTexts { get; set; }
        public string LineNumber { get; set; } = "30004505000017";
    }
}
