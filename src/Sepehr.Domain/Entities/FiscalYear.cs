﻿using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class FiscalYear:BaseEntity<int>
    {
        public int Year { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
