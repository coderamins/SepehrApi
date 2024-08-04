using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.RentPayment
{
    public class RentPaymentDto
    {
        public int ReceivePaymentOriginId { get; set; }
        public Guid? TransferRemittanceUnloadingPermitId { get; set; }
        public Guid? LadingExitPermitId { get; set; } 

        public required decimal TotalFareAmount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
