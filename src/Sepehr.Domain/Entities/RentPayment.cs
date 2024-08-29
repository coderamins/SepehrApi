using Sepehr.Domain.Common;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class RentPayment:AuditableBaseEntity<int>
    {
        public required decimal TotalFareAmount { get; set; }
        public EFareAmountStatus FareAmountStatusId { get; set; } = EFareAmountStatus.Payed;

        #region نوع و مبدا پرداخت
        public int? PaymentOriginTypeId { get; set; }
        public Guid? PaymentFromCustomerId { get; set; }
        public int? PaymentFromOrganizationBankId { get; set; }
        public int? PaymentFromCashDeskId { get; set; }
        public int? PaymentFromIncomeId { get; set; }
        public int? PaymentFromPettyCashId { get; set; }
        public int? PaymentFromCostId { get; set; }
        public Guid? PaymentFromShareHolderId { get; set; }

        public virtual PaymentOriginType? PaymentOriginType { get; set; }
        public virtual Customer? PaymentFromCustomer { get; set; }
        public virtual OrganizationBank? PaymentFromOrganizationBank { get; set; }
        public virtual CashDesk? PaymentFromCashDesk { get; set; }
        public virtual Income? PaymentFromIncome { get; set; }
        public virtual PettyCash? PaymentFromPettyCash { get; set; }
        public virtual Cost? PaymentFromCost { get; set; }
        public virtual ShareHolder? PaymentFromShareHolder { get; set; }

        #endregion

        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Attachment> Attachments { get; set; }= new List<Attachment>();
        public virtual ICollection<RentPaymentDetail> RentPaymentDetails { get; set; }=new List<RentPaymentDetail>();
 
    }
}
