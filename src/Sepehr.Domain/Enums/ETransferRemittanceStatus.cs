using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Enums
{
    public enum ETransferRemittanceStatus
    {
        /// <summary>
        /// در حال بررسی
        /// </summary>
        InProgress = 1,
        /// <summary>
        /// ثبت ورود شده
        /// </summary>
        Entranced = 2,
        /// <summary>
        /// تخلیه شده
        /// </summary>
        Unloaded = 3,
        /// <summary>
        /// ابطال شده
        /// </summary>
        Canceled = 10,
    }
}
