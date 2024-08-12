using Sepehr.Domain.Common;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class Attachment : BaseEntity<Guid>
    {
        public required byte[] FileData { get; set; }
        public Guid? ReceivePayId { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public int? LadingPermitId { get; set; }
        public Guid? PurOrderTransRemittanceUnloadingPermitId { get; set; }
        public Guid? PurOrderTransRemittanceEntrancePermitId { get; set; }
        public Guid? LadingExitPermitId { get; set; }
        public AttachmentType? AttachmentType { get; set; } = 0;
        public Guid? CargoAnnounceId { get; set; }
        public Guid? PaymentRequestId { get; set; }

        public virtual LadingExitPermit? LadingExitPermit { get; set; }
        public virtual ReceivePay? ReceivePay { get; set; }
        public virtual Order? Order { get; set; }
        public virtual LadingPermit? LadingPermit { get; set; }
        public virtual EntrancePermit? PurOrderTransRemittanceEntrancePermit { get; set; }
        public virtual UnloadingPermit? PurOrderTransRemittanceUnloadingPermit { get; set; }
        public virtual PaymentRequest? PaymentRequest { get; set; }

    }
}
