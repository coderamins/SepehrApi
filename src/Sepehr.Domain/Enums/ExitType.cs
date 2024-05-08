using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Enums
{
    public enum ExitType
    {
        /// <summary>
        /// در این حالت کنترلی انجام نمی شود
        /// </summary>
        Usual = 1,
        /// <summary>
        /// هنگام اعلام بار در صورتیکه تسویه انجام نشده باشد ، قابل اعلام بار نباشد
        /// </summary>
        CargoAnncAfterSettlement =2,
        /// <summary>
        /// هنگام مجوز بارگیری در صورتیکه تسویه انجام نشده باشد ، قابل بارگیری نباشد
        /// </summary>
        ExitAfterSettlement =2
    }
}
