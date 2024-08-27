using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.Phonebook
{
    public class PhonebookDto
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public EPhoneNoType PhoneNumberTypeId { get; set; } = EPhoneNoType.Office;

        public List<InternalPhoneNumberDto> InternalPhones { get; set; }

    }
}
