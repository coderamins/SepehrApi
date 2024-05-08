using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class CargoExitPermit : AuditableBaseEntity<int>
    {
        public required int LadingPermitId { get; set; }
        public required string BankAccountNo { get; set; }
        [StringLength(16)]
        public required string CreditCardNo { get; set; }
        public required string BankAccountOwnerName { get; set; }
        public decimal OtherAmount { get; set; }
        public string? Description { get; set; }


        public required virtual LadingPermit LadingLicense { get; set; }
        public required virtual ICollection<CargoExitPermitDetail> CargoExitPermitDetails { get; set; }
    }
}
