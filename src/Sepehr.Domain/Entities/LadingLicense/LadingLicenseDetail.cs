using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class LadingLicenseDetail : BaseEntity<int>
    {
        public int LadingLicenseId { get; set; }
        public int OrderDetailId { get; set; }

        [DataType(DataType.Currency)]
        public decimal LadingAmount { get; set; }

        /// <summary>
        /// هنگام صدور مجوز خروج ثبت می شود
        /// </summary>uu
        [DataType(DataType.Currency)]
        public decimal RealAmount { get; set; }
        public int PackageCount { get; set; }

        public required virtual LadingLicense LadingLicense { get; set; }
        public required virtual OrderDetail OrderDetail { get; set; }
    }
}
