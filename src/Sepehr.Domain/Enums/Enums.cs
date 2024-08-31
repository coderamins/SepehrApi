using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Enums
{
    public enum EBalanceResult
    {
        debit=1,
        credit
    }

    public enum EBalanceReportType
    {
        ByApproximate=1,
        WitoutApproximate
    }

    public enum EBalanceType
    {
    }

    public enum EBalanceReportType
    {
        ByApproximate = 1,
        WithoutApproximate
    }

    public enum ECustomerValidity
    {
        Usual=1,
        undesirable,
        Golden,
        VIP,
        BlackListed
    }
    public enum EFarePaymentType
    {
        /// <summary>
        /// کرایه با خودمان
        /// </summary>
        FareByOurselves = 1,
        /// <summary>
        /// کرایه با مشتری
        /// </summary>
        FareWithCustomer = 2,
    }

    public enum EFareAmountStatus
    {
        InProgress = 1,
        Approved = 2,
        Payed = 3,
    }

    public enum EPaymentOriginType
    {
        /// <summary>
        /// مشتری
        /// </summary>
        Customer = 1,
        /// <summary>
        /// بانک
        /// </summary>
        Bank,
        /// <summary>
        /// صندوق
        /// </summary>
        CashDesk,
        /// <summary>
        /// درآمد
        /// </summary>
        Income,
        /// <summary>
        /// 
        /// </summary>
        PettyCash,
        /// <summary>
        /// هزینه
        /// </summary>
        Cost,
        /// <summary>
        /// برداشت نقدی سهامداران
        /// </summary>
        ShareHolderCashPay,
        /// <summary>
        /// پرداخت خمس سهامداران
        /// </summary>
        ShareHolderKhumsPay
    }
}
