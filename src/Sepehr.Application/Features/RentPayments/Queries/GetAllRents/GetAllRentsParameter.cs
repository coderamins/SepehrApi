using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.RentPayments.Queries.GetAllRentPayments
{
    public class GetAllRentsParameter :RequestParameter
    {
        public Guid? LadingExitPermitId { get; internal set; }
        public Guid? PurchaseOrderTransferRemittanceUnloadingPermitId { get; internal set; }
    }
}