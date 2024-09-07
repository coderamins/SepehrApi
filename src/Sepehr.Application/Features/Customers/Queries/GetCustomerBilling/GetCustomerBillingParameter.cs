using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Customers.Queries.GetAllCustomers
{
    public class GetCustomerBillingParameter : RequestParameter
    {
        public Guid CustomerId { get; set; }
        public string FromDate { get; set; } = string.Empty;
        public string ToDate { get; set; } = string.Empty;
        public int BillingReportType { get; set; }

    }
}