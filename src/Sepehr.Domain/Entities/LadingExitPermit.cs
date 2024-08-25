using Audit.EntityFramework;
using Sepehr.Domain.Common;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class LadingExitPermit:AuditableBaseEntity<Guid>
    {
        public int LadingExitPermitCode { get; set; }
        public int LadingPermitId { get; set; }
        public string? BankAccountNo { get; set; }
        public string? CreditCardNo { get; set; }
        public string? BankAccountOwnerName { get; set; }
        public decimal? OtherAmount { get; set; }
        public string? ExitPermitDescription { get; set; }
        public decimal? FareAmount { get; set; }
        public int? FareAmountStatusId { get; set; }=(int)EFareAmountStatus.InProgress;
        public bool? HasExitPermit { get; set; }

        //[AuditIgnore]
        //public ICollection<DriverFareAmountApprove>? DriverFareAmountApproves { get; set; }
        public virtual FareAmountStatus? FareAmountStatus { get; set; }
        [AuditIgnore] 
        public required virtual ICollection<LadingExitPermitDetail> LadingExitPermitDetails { get; set; }
        public required virtual LadingPermit LadingPermit { get; set; }
        [AuditIgnore]
        public virtual ICollection<Attachment>? Attachments { get; set; }
    }
}
