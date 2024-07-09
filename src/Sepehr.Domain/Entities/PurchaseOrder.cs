using Sepehr.Domain.Common;
using Sepehr.Domain.Entities.BaseEntities;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class PurchaseOrder:OrderBaseEntity
    {
        public ExitType ExitType { get; set; }
        public int OriginWarehouseId { get; set; }
        public int DestinationWarehouseId { get; set; }
        /// <summary>
        /// سفارش واسطه ای می باشد یا نه
        /// </summary>
        public bool IsIntermediary { get; set; } = false;

        /// <summary>
        /// کاربر تایید کننده
        /// </summary>
        //[ForeignKey("User")]
        public Guid? ApprovingUserId { get; set; }


        public required virtual Warehouse OriginWarehouse { get; set; }
        public required virtual Warehouse DestinationWarehouse { get; set; }
        public required virtual CustomerOfficialCompany? CustomerOfficialCompany { get; set; }
        public required virtual PurchaseOrderSendType OrderSendType { get; set; }
        public required virtual InvoiceType InvoiceType { get; set; }
        public  required virtual Customer Customer { get; set; }
        public required virtual PurchaseOrderFarePaymentType FarePaymentType { get; set; }
        public required virtual PurchaseOrderStatus OrderStatus { get; set; }
        public virtual ICollection<LadingPermit> LadingLicenses  { get; set; }= new List<LadingPermit>();
        public required virtual ICollection<PurchaseOrderDetail> Details { get; set; }
        public required virtual ICollection<PurchaseOrderPayment> OrderPayments { get; set; }=new List<PurchaseOrderPayment>();
        public virtual ICollection<PurchaseOrderService> OrderServices { get; set; }=new HashSet<PurchaseOrderService>();
        public virtual ICollection<CargoAnnounce> CargoAnnounces { get; set; }= new HashSet<CargoAnnounce>();
        public virtual ICollection<Attachment> Attachments { get; set; }=new HashSet<Attachment>();
    }
}
