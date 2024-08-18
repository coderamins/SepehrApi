﻿using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class PaymentRequestReason:BaseEntity<int>
    {
        public required string ReasonDesc { get; set; }
    }
}