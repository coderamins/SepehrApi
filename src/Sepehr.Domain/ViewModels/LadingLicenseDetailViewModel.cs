using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class LadingLicenseDetailViewModel
    {
        public int Id { get; set; }
        public int LadingLicenseId { get; set; }
        public int OrderDetailId { get; set; }

        public decimal LadingAmount { get; set; }

        /// <summary>
        /// هنگام صدور مجوز خروج ثبت می شود
        /// </summary>uu
        public decimal RealAmount { get; set; }
        public int PackageCount { get; set; }

        public required virtual OrderDetailViewModel OrderDetail { get; set; }

    }
}
