using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class RentPaymentViewModel
    {
        public int Id { get; set; }
        public string CreatedDate { get; set; } = string.Empty;
        public Guid? UnloadingPermitId { get; set; }
        public Guid? LadingExitPermitId { get; set; }
        public EPaymentOriginType PaymentOriginTypeId { get; set; }
        public string PaymentOriginTypeDesc { get; set; } = string.Empty;
        public string PaymentOriginId { get; set; } = string.Empty;
        public string PaymentOriginDesc { get; set; } = string.Empty;

        public decimal TotalFareAmount { get; set; }
        public decimal OtherCosts { get; set; }
        public string CreatorName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<RentPaymentDetailViewModel> RentPaymentDetails { get; set; }
    }
}
