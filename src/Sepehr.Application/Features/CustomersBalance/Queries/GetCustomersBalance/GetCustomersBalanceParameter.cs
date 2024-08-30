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
        public int? CustomerCode { get; set; }
        public Guid? CustomerId { get; set; }
        public string BalanceDate { get; set; } = string.Empty;
        public EBalanceReportType EBalanceReportType { get; set; }

        public int? CustomerLabelId { get; set; }
        public CustomerReportType ReportType { get; set; }
    }
}