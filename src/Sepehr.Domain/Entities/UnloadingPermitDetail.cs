using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class UnloadingPermitDetail : BaseEntity<int>
    {
        public Guid UnloadingPermitId { get; set; }
        public int TransferRemittanceDetailId { get; set; }
        public decimal UnloadedAmount { get; set; }

        public virtual required UnloadingPermit UnloadingPermit { get; set; }
        public virtual required TransferRemittanceDetail TransferRemittanceDetail { get; set; }

    }
}
