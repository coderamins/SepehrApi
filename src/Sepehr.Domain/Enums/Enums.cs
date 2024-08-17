using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Enums
{

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
