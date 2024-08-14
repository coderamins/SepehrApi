using Sepehr.Domain.Common;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities.BaseEntities
{
    public class PaymentRequestBase:AuditableBaseEntity<Guid>
    {
        public int PaymentRequestCode { get; set; }
        public EPaymentRequestType PaymentRequestTypeId { get; set; } = EPaymentRequestType.formal;
        public decimal Amount { get; set; }
        public int PaymentRequestReasonId { get; set; }
        public required string BankAccountOrShabaNo { get; set; }
        public string AccountOwnerName { get; set; } = string.Empty;
        public int BankId { get; set; }
        public string ApplicatorName { get; set; } = string.Empty;
        public Guid? ApproverId { get; set; }
        public int PaymentRequestStatusId { get; set; } = 1;
        public string PersonnelPaymentRequestDescription { get; set; } = string.Empty;
        public string RejectReasonDesc { get; set; } = string.Empty;

        public required virtual PaymentRequestReason PaymentRequestReason { get; set; }
        public required virtual PaymentRequestStatus PaymentRequestStatus { get; set; }
        public required virtual Bank Bank { get; set; }
        public virtual ApplicationUser? Approver { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();

    }
}
