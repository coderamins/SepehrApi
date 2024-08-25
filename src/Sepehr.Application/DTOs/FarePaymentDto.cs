using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs
{
    public class FarePaymentDto
    {
        public Guid? UnloadingPermitId { get; set; }
        public Guid? LadingExitPermitId { get; set; }
        public required decimal TotalFareAmount { get; set; }
        public EFareAmountStatus FareAmountStatusId { get; set; } = EFareAmountStatus.InProgress;

        public string PaymentOriginId { get; set; } = string.Empty;
        public int? PaymentOriginTypeId { get; set; }
    }
}
