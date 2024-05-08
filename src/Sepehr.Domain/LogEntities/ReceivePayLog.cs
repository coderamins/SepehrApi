using Sepehr.Domain.Common;
using Sepehr.Domain.LogEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class ReceivePayLog:AuditableBaseEntityLog<Guid>
    {
        public Guid? ReceiveFromCustomerId { get; set; }
        public long ReceivePayCode { get; set; }
        public Guid? PayToCustomerId { get; set; }
        public int? ReceivePaymentSourceFromId { get; set; }
        public int? ReceivePaymentSourceToId { get; set; }
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public string? AccountOwner { get; set; }
        public string? TrachingCode { get; set; }
        public string? CompanyName { get; set; }
        public string? ContractCode { get; set; }
        public bool IsAccountingApproval { get; set; } = false;
        public DateTime AccountingApprovalDate { get; set; }
        public string? AccountingApproverId { get; set; }
        public string? Description { get; set; }
        public int LogTypeId { get; set; }
        public virtual LogType LogType { get; set; }


    }
}
