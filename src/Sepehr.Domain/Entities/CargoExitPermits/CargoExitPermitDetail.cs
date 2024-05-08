using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class CargoExitPermitDetail : BaseEntity<int>
    {
        //public int CargoExitPermitId { get; set; }
        public Guid LadingExitPermitId { get; set; }
        public int LadingPermitDetailId { get; set; }
        [DataType(DataType.Currency)]
        public decimal LadingAmount { get; set; }

        /// <summary>
        /// هنگام صدور مجوز خروج ثبت می شود
        /// </summary>uu
        [DataType(DataType.Currency)]
        public decimal RealAmount { get; set; }
        public int PackageCount { get; set; }

        public virtual required LadingLicenseDetail LadingLicenseDetail { get; set; }
        public required virtual LadingExitPermit LadingExitPermit { get; set; }
        public required virtual OrderDetail OrderDetail { get; set; }

        //public required virtual CargoExitPermit CargoExitPermit { get; set; }

    }
}
