using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Customers.Command.CreateCustomer
{
    public class CreatePhonebookRequest
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsPrimary { get; set; } = false;
        public int PhoneNumberTypeId { get; set; }
    }
}
