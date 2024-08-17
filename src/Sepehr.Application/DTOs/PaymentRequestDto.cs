using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs
{
    public class PaymentRequestDto
    {
        public decimal Amount { get; set; }
        public string PaymentRequestReasonDesc { get; set; } = string.Empty;
        public required string BankAccountOrShabaNo{ get; set; }
        public required string AccountOwnerName{ get; set; }
        public EPaymentRequestType PaymentRequestTypeId{ get; set; }
        public string PaymentRequestDescription{ get; set; }=string.Empty;
    }
}
