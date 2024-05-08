using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Enums
{
    public enum PurchaseOrderStatusEnum
    {
        /// <summary>
        /// جدید 
        /// </summary>
        NewOrder = 1,

        /// <summary>
        /// تایید شده حسابداری
        /// </summary>
        AccApproved,

        /// <summary>
        /// تایید نشده حسابداری
        /// </summary>
        AccNotApproved,
        
        /// <summary>
        /// انتقال داده شده به انبار
        /// </summary>
        TransferedToWarehouse,

        /// <summary>
        /// ابطال شده
        /// </summary>
        Canceled,

        /// <summary>
        /// ابطال شده
        /// </summary>
        ReturnedBack,
    }
}
