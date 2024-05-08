using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.RentPayments.Queries.GetAllRentPayments
{
    public class GetAllRentPaymentsParameter :RequestParameter
    {
        public int? RentPaymentCode { get; set; }
        public int? RentPaymentId { get; internal set; }
        public int? ReferenceCode { get; set; }
        public string FromDate { get; set; } = string.Empty;
        public string ToDate { get; set; } = string.Empty;
        public string DriverName { get; set; } = string.Empty;
    }
}