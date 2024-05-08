using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sepehr.Domain.Settings
{
    public class SmsSettings
    {
        public required string sender { get; set; }
        public required string receptor { get; set; }
        public required string apikey { get; set; }
        public required string amootToken { get; set; }
        public required string amootLineNumber { get; set; }
    }
}