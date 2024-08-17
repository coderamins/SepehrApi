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
        [JsonIgnore]
        public int PaymentRequestCode { get; set; }
        public EPaymentRequestType PaymentRequestTypeId { get; set; } = EPaymentRequestType.formal;
        public decimal Amount { get; set; }
        public required string BankAccountOrShabaNo { get; set; }
        public string PaymentRequestReasonDesc { get; set; } = string.Empty;
        public string AccountOwnerName { get; set; } = string.Empty;
        [JsonIgnore]
        public Guid? ApproverId { get; set; }
        [JsonIgnore]
        public int PaymentRequestStatusId { get; set; } = 1;
        public string PaymentRequestDescription { get; set; } = string.Empty;
        [JsonIgnore]
        public string RejectReasonDesc { get; set; } = string.Empty;

        #region نوع و مبدا پرداخت
        [JsonIgnore]
        public int? PaymentOriginTypeId { get; set; }

        [JsonIgnore]
        public Guid? PaymentFromCustomerId { get; set; }
        [JsonIgnore]
        public int? PaymentFromOrganizationBankId { get; set; }
        [JsonIgnore]
        public int? PaymentFromCashDeskId { get; set; }
        [JsonIgnore]
        public int? PaymentFromIncomeId { get; set; }
        [JsonIgnore]
        public int? PaymentFromPettyCashId { get; set; }
        [JsonIgnore]
        public int? PaymentFromCostId { get; set; }
        [JsonIgnore]
        public Guid? PaymentFromShareHolderId { get; set; }

                
        [JsonIgnore]
        public virtual PaymentOriginType? PaymentOriginType { get; set; }
        [JsonIgnore]
        public virtual Customer? PaymentFromCustomer { get; set; }
        [JsonIgnore]
        public virtual OrganizationBank? PaymentFromOrganizationBank { get; set; }
        [JsonIgnore]
        public virtual CashDesk? PaymentFromCashDesk { get; set; }
        [JsonIgnore]
        public virtual Income? PaymentFromIncome { get; set; }
        [JsonIgnore]
        public virtual PettyCash? PaymentFromPettyCash { get; set; }
        [JsonIgnore]
        public virtual Cost? PaymentFromCost { get; set; }
        [JsonIgnore]
        public virtual ShareHolder? PaymentFromShareHolder { get; set; }

        #endregion


        [JsonIgnore]
        public required virtual PaymentRequestStatus PaymentRequestStatus { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser? Approver { get; set; }
        [JsonIgnore]
        public virtual ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();

    }
}
