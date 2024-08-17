using Sepehr.Domain.Common;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities.BaseEntities
{
    public class PaymentRequestBase:AuditableBaseEntity<Guid>
    {
        public int PaymentRequestCode { get; set; }
        public EPaymentRequestType PaymentRequestTypeId { get; set; } = EPaymentRequestType.formal;
        public decimal Amount { get; set; }
        public required string BankAccountOrShabaNo { get; set; }
        public string PaymentRequestReasonDesc { get; set; } = string.Empty;
        public string AccountOwnerName { get; set; } = string.Empty;
        public Guid? ApproverId { get; set; }
        public int PaymentRequestStatusId { get; set; } = 1;
        public string PaymentRequestDescription { get; set; } = string.Empty;
        public string RejectReasonDesc { get; set; } = string.Empty;

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


        public required virtual PaymentRequestStatus PaymentRequestStatus { get; set; }
        public virtual ApplicationUser? Approver { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();

    }
}
