﻿using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class FareAmountStatus:BaseEntity<int>
    {
        public string StatusDesc { get; set; } = string.Empty;
    }
}
