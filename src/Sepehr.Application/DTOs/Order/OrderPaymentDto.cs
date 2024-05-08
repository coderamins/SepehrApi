using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.Order
{
    public class OrderPaymentDto
    {
        public Guid? Id { get; set; } = null;
        public required decimal Amount { get; set; }
        public string? PaymentDate { get; set; }
        public int DaysAfterExit { get; set; }
        public PaymentType PaymentType { get; set; } = PaymentType.CashePay;
    }
}
