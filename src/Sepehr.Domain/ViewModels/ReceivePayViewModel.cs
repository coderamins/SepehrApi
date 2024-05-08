using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class ReceivePayViewModel
    {
        public Guid Id { get; set; }        
        public long ReceivePayCode { get; set; }

        /// <summary>
        /// نوع دریافت از 
        /// </summary>
        public int? ReceivePaymentTypeFromId { get; set; }
        public string ReceivePaymentTypeFromDesc { get; set; } = string.Empty;

        /// <summary>
        /// نوع پرداخت به
        /// </summary>
        public int? ReceivePaymentTypeToId { get; set; }
        public string ReceivePaymentTypeToDesc { get; set; } = string.Empty;


        #region  دریافت از
        public string ReceiveFromId { get; set; } = string.Empty;
        public string ReceiveFromDesc { get; set; } = string.Empty;
        #endregion

        #region پرداخت به
        public string PayToId { get; set; } = string.Empty;
        public string PayToDesc { get; set; } = string.Empty;
        #endregion


        public decimal Amount { get; set; }
        public int? ReceivePayStatusId { get; set; }
        public string ReceivePayStatusDesc { get; set; } = string.Empty;

        public string? AccountOwner { get; set; }
        public string? TrachingCode { get; set; }
        public string? CompanyName { get; set; }
        public string? ContractCode { get; set; }
        public bool IsAccountingApproval { get; set; } = false;
        public string? AccountingApprovalDate { get; set; }
        public Guid? AccountingApproverId { get; set; }
        public string? Description { get; set; }
        public bool HasAttachment { get; set; }

        public int? ReceiveFromCompanyId { get; set; }
        public string? ReceiveFromCompanyName { get; set; }
        public int? PayToCompanyId { get; set; }
        public string? PayToCompanyName { get; set; }

        public int? AccountingDocNo { get; set; }
        public string AccountingDescription { get; set; } = string.Empty;

        public List<AttachmentViewModel>? Attachments { get; set; }
    }
}
