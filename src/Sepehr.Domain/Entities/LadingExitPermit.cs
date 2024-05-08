using Sepehr.Domain.Common;
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
        public bool FareAmountPayStatus { get; set; } = false;
        public bool? HasExitPermit { get; set; }
        public bool FareAmountApproved { get; set; }

        public ICollection<DriverFareAmountApprove>? DriverFareAmountApproves { get; set; }
        public required virtual ICollection<LadingExitPermitDetail> LadingExitPermitDetails { get; set; }
        public required virtual LadingPermit LadingPermit { get; set; }
        public virtual ICollection<Attachment>? Attachments { get; set; }
    }
}
