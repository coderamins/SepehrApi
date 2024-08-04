using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class PhonebookViewModel
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public int PhoneNumberTypeId { get; set; }
        public string PhoneNumberTypeDesc { get; set; }
    }
}
