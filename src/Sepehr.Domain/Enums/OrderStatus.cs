using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Enums
{
    public enum OrderStatusEnum
    {
        /// <summary>
        /// جدید 
        /// </summary>
        NewOrder = 1,
        /// <summary>
        /// تایید شده 
        /// </summary>
        Confirmed,
        /// <summary>
        /// تایید شده حسابداری
        /// </summary>
        AccApproved,
        /// <summary>
        /// تایید نشده حسابداری
        /// </summary>
        AccNotApproved,
        /// <summary>
        /// در حال ارسال
        /// </summary>
        Sending,
        /// <summary>
        /// ارسال شده
        /// </summary>
        Sended,
        /// <summary>
        /// برگشت خورده
        /// </summary>
        ReturnedBack,
        /// <summary>
        /// ابطال شده
        /// </summary>
        Canceled,
        /// <summary>
        /// مقداری از بار برگشت خورده
        /// </summary>
        SomeItemsCanceled
    }
}
