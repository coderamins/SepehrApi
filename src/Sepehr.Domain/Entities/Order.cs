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
    public class Order : AuditableBaseEntity<Guid>
    {
        public long OrderCode { get; set; }
        public Guid CustomerId { get; set; }
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }
        public int OrderSendTypeId { get; set; }
        public int OrderStatusId { get; set; } = 1;
        public int FarePaymentTypeId { get; set; }
        public int InvoiceTypeId { get; set; }
        public int? CustomerOfficialCompanyId { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string? Barcode { get; set; }
        public string? Description { get; set; }
        /// <summary>
        /// توضیحات تایید فاکتور سفارش
        /// </summary>
        public string InvoiceApproveDescription { get; set; } = string.Empty;

        /// <summary>
        /// آیا سفارش موقت می باشد؟
        /// </summary>
        public bool IsTemporary { get; set; }

        public OrderType OrderTypeId { get; set; }
        public DateTime DeliverDate { get; set; }
        public int OrderExitTypeId { get; set; }

        public virtual CustomerOfficialCompany? CustomerOfficialCompany { get; set; }
        public required virtual OrderExitType OrderExitType { get; set; }
        public required virtual OrderSendType OrderSendType { get; set; }
        public required virtual InvoiceType InvoiceType { get; set; }
        public required virtual Customer Customer { get; set; }
        public required virtual FarePaymentType FarePaymentType { get; set; }
        public required virtual OrderStatus OrderStatus { get; set; }
        public virtual ICollection<LadingLicense> LadingLicenses { get; set; } = new List<LadingLicense>();
        public required virtual ICollection<OrderDetail> Details { get; set; }
        public required virtual ICollection<OrderPayment> OrderPayments { get; set; }
        public virtual ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();
        public virtual ICollection<CargoAnnounce> CargoAnnounces { get; set; } = new List<CargoAnnounce>();
        public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
        public virtual ICollection<LadingPermit> LadingPermits { get; set; } = new List<LadingPermit>();


    }
}
