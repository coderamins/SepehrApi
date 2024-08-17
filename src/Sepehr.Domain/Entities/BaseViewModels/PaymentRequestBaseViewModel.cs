using Sepehr.Domain.Enums;

namespace Sepehr.Domain.Entities.BaseViewModels
{
    public class PaymentRequestBaseViewModel
    {
        public Guid Id { get; set; }
        public int PaymentRequestCode { get; set; }
        public EPaymentRequestType PaymentRequestTypeId { get; set; }
        public string PaymentRequestTypeDesc { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int? PaymentOriginTypeId { get; set; }
        public string PaymentOriginTypeDesc { get; set; } = string.Empty;
        public string PaymentOriginId { get; set; } = string.Empty;
        public string PaymentOriginDesc { get; set; } = string.Empty;
        public string PaymentRequestReasonDesc { get; set; } = string.Empty;
        public string BankAccountOrShabaNo { get; set; } = string.Empty;
        public string AccountOwnerName { get; set; } = string.Empty;
        public int BankId { get; set; }
        public string BankName { get; set; } = string.Empty;
        public Guid? ApproverId { get; set; }
        public string ApproverName { get; set; } = string.Empty;
        public int PaymentRequestStatusId { get; set; }
        public string PaymentRequestStatusDesc { get; set; } = string.Empty;
        public string PaymentRequestDescription { get; set; } = string.Empty;
        public string RejectReasonDesc { get; set; } = string.Empty;
        public string CreatorName { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
    }
}
