using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Customers.Queries.GetAllCustomers
{
    public class GetCustomersBalanceParameter : RequestParameter
    {
        public Guid? CustomerId { get; set; }
        public int? CustomerCode { get; set; }
        public string BalanceFromDate { get; set; } = string.Empty;
        public string BalanceToDate { get; set; } = string.Empty;
        public EBalanceReportType ReportType { get; set; }
    }
}