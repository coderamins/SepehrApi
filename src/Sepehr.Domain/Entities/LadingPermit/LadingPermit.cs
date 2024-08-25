using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class LadingPermit : AuditableBaseEntity<int>
    {
        public Guid CargoAnnounceId { get; set; }
        public bool? HasExitPermit { get; set; } = false;
        public string? Description { get; set; }
        //public decimal? FareAmount { get; set; }

        //#region هنگام صدور مجوز خروج ثبت می شود
        //public string? BankAccountNo { get; set; }
        //public string? CreditCardNo { get; set; }
        //public string? BankAccountOwnerName { get; set; }
        //public decimal? OtherAmount { get; set; }
        //public string? ExitPermitDescription { get; set; }
        //public int? ProductSubUnitId { get; set; }
        //#endregion

        //public required virtual ProductUnit ProductSubUnit { get; set; }
        //public required virtual ICollection<LadingPermitDetail> LadingPermitDetails { get; set; }
        public virtual ICollection<LadingExitPermit>? LadingExitPermit { get; set; }
        public virtual required CargoAnnounce CargoAnnounce { get; set; }
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    }
}
