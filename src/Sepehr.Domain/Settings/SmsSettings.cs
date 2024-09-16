using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sepehr.Domain.Settings
{
    public class SmsSettings
    {
        public string apiUrl { get; set; } = string.Empty;
        public required string sender { get; set; }
        public required string receptor { get; set; }
        public required string apikey { get; set; }
        public int SmsTemplateId { get; set; } 
        public required string Sender { get; set; }
    }
}