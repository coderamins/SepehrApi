using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sepehr.Domain.Common;

namespace Sepehr.Domain.Entities.BaseEntities
{
    public class OrderBaseEntity:AuditableBaseEntity<Guid>
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
    }
}
