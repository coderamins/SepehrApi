using Sepehr.Domain.Common;
using Sepehr.Domain.Enums;
using Sepehr.Domain.LogEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class OrderLog:AuditableBaseEntityLog<Guid>
    {
        public long OrderCode { get; set; }
        public Guid CustomerId { get; set; }
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }
        public bool ConfirmedStatus { get; set; } = false;
        public ExitType ExitType { get; set; }
        public int OrderSendTypeId { get; set; }
        public OrderType OrderTypeId { get; set; } 
        public int PaymentTypeId { get; set; }
        public string? CustomerOfficialName { get; set; }
        public int InvoiceTypeId { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string? FreightName { get; set; }
        public required DateTime SettlementDate { get; set; }
        public int OrderStatusId { get; set; } = 1;

        //واحد انتخاب شده
        public int? SelectedProductUnitId { get; set; }

        /// <summary>
        /// در صورتی که ارسال توسط مهفام باشد
        /// </summary>
        public string? DischargePlaceAddress { get; set; }
        /// <summary>
        /// در صورتی که ارسال به عهده مشتری باشد
        /// </summary>
        public string? FreightDriverName { get; set; }
        /// <summary> در صورتی که ارسال به عهده مشتری باشد </summary>
        public string? CarPlaque { get; set; }
        public string? Barcode { get; set; }
        public string? Description { get; set; }

        /// <summary>
        /// درصورتی که بار بصورت کامل ارسال شده باشد این فیلد مقدار صحیح میگیرد
        /// </summary>
        public bool IsCompletlySended { get; set; } = false;

        /// <summary>
        /// وضعیت تایید نوع فاکتور
        /// </summary>
        public bool IsApprovedInvoiceType { get; set; }

        /// <summary>
        /// کاربر تایید کننده
        /// </summary>
        //[ForeignKey("User")]
        public Guid? ApprovingUserId { get; set; }

        public int LogTypeId { get; set; }
        public virtual LogType LogType { get; set; }

    }
}
