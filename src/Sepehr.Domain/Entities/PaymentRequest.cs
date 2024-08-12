﻿using Sepehr.Domain.Common;
using Sepehr.Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class PaymentRequest:AuditableBaseEntity<Guid>
    {
        public Guid CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentReason { get; set; }=string.Empty;
        public required string BankAccountOrShabaNo { get; set; }
        public string AccountOwnerName { get; set; }= string.Empty;
        public int BankId { get; set; }
        public string ApplicatorName { get; set; } = string.Empty;
        public Guid? ApproverId { get; set; }
        public int PaymentRequestStatusId { get; set; }
        public string PaymentRequestDescription { get; set; } = string.Empty;
        public string RejectReasonDesc { get; set; }=string.Empty;  

        public required virtual PaymentRequestStatus PaymentRequestStatus { get; set; }
        public required virtual Bank Bank { get; set; } 
        public virtual ApplicationUser? Approver { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }=new HashSet<Attachment>();

    }
}
