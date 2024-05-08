using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class PurchaseOrderTransferRemittanceEntrancePermit:AuditableBaseEntity<Guid>
    {
        public int PermitCode { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseOrderTransferRemittanceId { get; set; }        
        
        public virtual ICollection<Attachment>? Attachments { get; set; }
        public required virtual PurchaseOrderTransferRemittance PurchaseOrderTransferRemittance { get; set; }
        public virtual ICollection<PurchaseOrderTransferRemittanceUnloadingPermit>? PurchaseOrderTransferRemittanceUnloadingPermits { get; set; }
    }
}
