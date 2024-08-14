using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.PersonnelPaymentRequests.Queries.GetAllPersonnelPaymentRequests
{
    public class GetAllPersonnelPaymentRequestsParameter : RequestParameter
    {
        public int? PaymentRequestCoode { get; set; }
    }
}