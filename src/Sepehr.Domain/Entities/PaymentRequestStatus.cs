﻿using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class PaymentRequestStatus:BaseEntity<int>
    {
        public required string StatusDesc { get; set; }
    }
}
