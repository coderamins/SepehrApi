using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class RentPayment:AuditableBaseEntity<int>
    {
        public int ReceivePaymentOriginId { get; set; }
        /// <summary>
        /// شماره مجوز تخلیه
        /// </summary>
        public Guid? UnloadingPermitId { get; set; }
        /// <summary>
        /// شماره مجوز خروج اعلام بار
        /// </summary>
        public Guid? LadingExitPermitId { get; set; }

        public required decimal TotalFareAmount { get; set; }


        public string Description { get; set; } = string.Empty;

        public required virtual PaymentOriginType ReceivePaymentOrigin { get; set; }
        public virtual LadingExitPermit? LadingExitPermit { get; set; }
        public virtual UnloadingPermit? UnloadingPermit { get; set; }
    }
}
