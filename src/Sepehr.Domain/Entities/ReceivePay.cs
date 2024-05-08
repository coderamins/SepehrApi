using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class ReceivePay:AuditableBaseEntity<Guid>
    {
        /// <summary>
        /// نوع دریافت از 
        /// </summary>
        public int? ReceivePaymentTypeFromId { get; set; }

        /// <summary>
        /// نوع پرداخت به
        /// </summary>
        public int? ReceivePaymentTypeToId { get; set; }

        public long ReceivePayCode { get; set; }
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public string? AccountOwner { get; set; }
        public string? TrachingCode { get; set; }
        public string? ContractCode { get; set; }
        public bool IsAccountingApproval { get; set; } = false;
        public DateTime AccountingApprovalDate { get; set; }
        public string? AccountingApproverId { get; set; }
        public int? AccountingDocNo { get; set; }
        public string? Description { get; set; }
        public string AccountingDescription { get; set; } = string.Empty;
        public int ReceivePayStatusId { get; set; } = 1;
        public string ChequeNo { get; set; } = string.Empty;
        public DateTime? ChequeDate { get; set; }

        #region  دریافت از
        public Guid? ReceiveFromCustomerId { get; set; }
        public int? ReceiveFromOrganizationBankId { get; set; }
        public int? ReceiveFromCashDeskId { get; set; }
        public int? ReceiveFromIncomeId { get; set; }
        public int? ReceiveFromPettyCashId { get; set; }
        public int? ReceiveFromCostId { get; set; }
        public Guid? ReceiveFromShareHolderId { get; set; }
        #endregion

        #region پرداخت به
        public Guid? PayToCustomerId { get; set; }
        public int? PayToOrganizationBankId { get; set; }
        public int? PayToCashDeskId { get; set; }
        public int? PayToIncomeId { get; set; }
        public int? PayToPettyCashId { get; set; }
        public int? PayToCostId { get; set; }
        public Guid? PayToShareHolderId { get; set; }
        #endregion

        #region اگر مقصد یکی یا هر دو طرف مشتری باشد
        public int? ReceiveFromCompanyId { get; set; }
        public int? PayToCompanyId { get; set; }
        #endregion


        #region مبدا و مقصد دریافت و پرداخت
        /// <summary>
        ///  دریافت از
        /// </summary>
        public virtual required ReceivePaymentType ReceivePaymentTypeFrom { get; set; }

        /// <summary>
        /// پرداخت به
        /// </summary>
        public virtual required ReceivePaymentType ReceivePaymentTypeTo { get; set; }
        #endregion

        #region  دریافت از
        public virtual Customer? ReceiveFromCustomer { get; set; }
        public virtual OrganizationBank? ReceiveFromOrganizationBank { get; set; }
        public virtual CashDesk? ReceiveFromCashDesk { get; set; }
        public virtual Income? ReceiveFromIncome { get; set; }
        public virtual PettyCash? ReceiveFromPettyCash { get; set; }
        public virtual Cost? ReceiveFromCost { get; set; }
        public virtual ShareHolder? ReceiveFromShareHolder { get; set; }
        public virtual CustomerOfficialCompany? ReceiveFromCompany { get; set; }
        public virtual CustomerOfficialCompany? PayToCompany { get; set; }
        #endregion

        #region پرداخت به
        public virtual Customer? PayToCustomer { get; set; }
        public virtual OrganizationBank? PayToOrganizationBank { get; set; }
        public virtual CashDesk? PayToCashDesk { get; set; }
        public virtual Income? PayToIncome { get; set; }
        public virtual PettyCash? PayToPettyCash { get; set; }
        public virtual Cost? PayToCost { get; set; }
        public virtual ShareHolder? PayToShareHolder { get; set; }
        #endregion

        public virtual ICollection<Attachment>? Attachments { get; set; }
        public virtual required ReceivePayStatus ReceivePayStatus { get; set; }

    }
}
