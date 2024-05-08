﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.Product
{
    public class TransferRemittanceDetailDto
    {
        public int? Id { get; set; }
        public int ProductBrandId { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
