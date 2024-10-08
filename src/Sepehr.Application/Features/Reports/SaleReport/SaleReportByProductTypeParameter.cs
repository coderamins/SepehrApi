﻿using Azure;
using MediatR;
using Sepehr.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Reports.SaleReport
{
    public class SaleReportByProductTypeParameter 
    {
        public string FromDate { get; set; } = string.Empty;
        public string ToDate { get; set; }=string.Empty;
        public decimal? OrderAmount { get; set; }
        public int? ProductTypeId { get; set; }
    }
}
