using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class RentPaymentDetail:BaseEntity<int>
    {
        public int RentPaymentId { get; set; }
        /// <summary>
        /// شماره مجوز تخلیه
        /// </summary>
        public Guid? UnloadingPermitId { get; set; }
        /// <summary>
        /// شماره مجوز خروج اعلام بار
        /// </summary>
        public Guid? LadingExitPermitId { get; set; }

        public virtual required RentPayment RentPayment  { get; set; }
        public virtual LadingExitPermit? LadingExitPermit { get; set; }
        public virtual UnloadingPermit? UnloadingPermit { get; set; }

    }
}
