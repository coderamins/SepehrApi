using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Enums
{
    public enum EReceivePayStatus
    {
        /// <summary>
        /// جدید
        /// </summary>
        New = 1,
        /// <summary>
        /// تایید شده
        /// </summary>
        Approved,
        /// <summary>
        /// تایید حسابداری
        /// </summary>
        AccApproved
    }
}
