using Sepehr.Domain.Common;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class PurchaseOrderPayment:BaseEntity<Guid>
    {
        public Guid OrderId { get; set; }
        [DataType(DataType.Currency)]
        public required decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int DaysAfterExit { get; set; }
        public PaymentType PaymentType { get; set; } = PaymentType.CashePay;

        public required virtual PurchaseOrder Order { get; set; }
    }
}
