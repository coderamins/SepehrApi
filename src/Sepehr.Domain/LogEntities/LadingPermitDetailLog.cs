using Sepehr.Domain.Common;
using Sepehr.Domain.LogEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class LadingPermitDetailLog : BaseEntity<int>
    {
        public int LadingPermitId { get; set; }
        public Guid ProductId { get; set; }
        /// <summary>
        /// هنگام صدور مجوز ثبت می شود
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal ApproximateAmount { get; set; }
        /// <summary>
        /// هنگام صدور خروج ثبت می شود
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal RealAmount { get; set; }
        public int PackageCount { get; set; }

        public int LogTypeId { get; set; }
        public virtual LogType LogType { get; set; }


    }
}
