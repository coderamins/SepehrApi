using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class RentPaymentViewModel : BaseViewModel<int>
    {
        public int RentPaymentCode { get; set; }
        public required decimal TotalFareAmount { get; set; }
        public EFareAmountStatus FareAmountStatusId { get; set; }
        public decimal OtherCosts { get; set; }

        #region نوع و مبدا پرداخت
        public int PaymentOriginId { get; set; }
        public string PaymentOriginDesc { get; set; } = string.Empty;
        public int? PaymentOriginTypeId { get; set; }
        public string PaymentOriginTypeDesc { get; set; } = string.Empty;
        public Guid? PaymentFromCustomerId { get; set; }
        public string PaymentFromCustomerName { get; set; } = string.Empty;
        public int? PaymentFromOrganizationBankId { get; set; }
        public string PaymentFromOrganizationBankDesc { get; set; } = string.Empty;
        public int? PaymentFromCashDeskId { get; set; }
        public string PaymentFromCashDeskDesc { get; set; } = string.Empty;
        public int? PaymentFromIncomeId { get; set; }
        public string PaymentFromIncomeDesc { get; set; } = string.Empty;
        public int? PaymentFromPettyCashId { get; set; }
        public string PaymentFromPettyCashDesc { get; set; } = string.Empty;
        public int? PaymentFromCostId { get; set; }
        public string PaymentFromCostDesc { get; set; }= string.Empty;
        public Guid? PaymentFromShareHolderId { get; set; }
        public string PaymentFromShareHolderName { get; set; }=string.Empty;

        #endregion

        public string Description { get; set; } = string.Empty;

        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
        public List<RentPaymentDetailViewModel> RentPaymentDetails { get; set; }=new List<RentPaymentDetailViewModel>();

    }
}
