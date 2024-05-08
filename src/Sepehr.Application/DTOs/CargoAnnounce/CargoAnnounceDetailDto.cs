using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.CargoAnnounce
{
    public class CargoAnnounceDetailDto
    {
        public Guid CargoAnnounceId { get; set; }
        public int OrderDetailId { get; set; }
        public decimal LadingAmount { get; set; }

        /// <summary>
        /// هنگام صدور مجوز خروج ثبت می شود
        /// </summary>uu
        [DataType(DataType.Currency)]
        public decimal RealAmount { get; set; }
        public int PackageCount { get; set; }
    }
}
