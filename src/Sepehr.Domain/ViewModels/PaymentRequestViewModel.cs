using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class PaymentRequestViewModel
    {
        public Guid Id { get; set; }
        public int PaymentRequestCode { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int PaymentRequestReasonId { get; set; } 
        public string PaymentRequestReasonDesc { get; set; } = string.Empty;
        public string BankAccountOrShabaNo { get; set; } = string.Empty;
        public string AccountOwnerName { get; set; } = string.Empty;
        public int BankId { get; set; }
        public string BankName { get; set; }=string.Empty;
        public string ApplicatorName { get; set; } = string.Empty;
        [ForeignKey("ApplicationUser")]
        public Guid? ApproverId { get; set; }
        public int PaymentRequestStatusId { get; set; }
        public string PaymentRequestStatusDesc { get; set; } = string.Empty;
        public string PaymentRequestDescription { get; set; } = string.Empty;
        public string RejectReasonDesc { get; set; } = string.Empty;


        public string CreatorName { get; set; }= string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
    }
}
