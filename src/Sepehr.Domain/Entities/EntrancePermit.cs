using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class EntrancePermit:AuditableBaseEntity<Guid>
    {
        public int PermitCode { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransferRemittanceId { get; set; }        
        
        public virtual ICollection<Attachment>? Attachments { get; set; }
        public required virtual TransferRemittance TransferRemittance { get; set; }
        public virtual UnloadingPermit? UnloadingPermit { get; set; }
    }
}
