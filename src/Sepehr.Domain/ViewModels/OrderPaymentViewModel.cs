using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class OrderPaymentViewModel
    {
        public Guid Id { get; set; }
        public required decimal Amount { get; set; }
        public string? PaymentDate { get; set; }
        public int DaysAfterExit { get; set; }
        public PaymentType PaymentType { get; set; } = PaymentType.CashePay;
    }
}
