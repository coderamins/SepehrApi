﻿using Sepehr.Domain.Common;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class Phonebook : AuditableBaseEntity<int>
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public Guid? CustomerId { get; set; }
        public Guid? PersonnelId { get; set; }
        public int PhoneNumberTypeId { get; set; }
        public bool IsPrimary { get; set; }

        public virtual required PhoneNumberType PhoneNumberType { get; set; }
        public virtual ICollection<InternalPhoneNumber> InternalPhones  { get; set; }  =new List<InternalPhoneNumber>();
        public virtual Customer? Customer { get; set; }
        public virtual Personnel? Personnel { get; set; }
    }
}
