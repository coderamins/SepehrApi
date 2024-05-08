using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.RentPayments.Queries.GetAllRentPayments
{
    public class GetAllRentsToPaymentParameter :RequestParameter
    {
        public int? ReferenceCode { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public string DriverMobile { get; set; } = string.Empty;
        public OrderClassType? OrderType { get; set; }
        public string FromDate { get; set; } = string.Empty;
        public string ToDate { get; set; }=string.Empty;
    }
}