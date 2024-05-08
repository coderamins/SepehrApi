using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.LadingLicense
{
    public class LadingLicenseDetailDto
    {
        public int OrderDetailId { get; set; }
        public decimal LadingAmount { get; set; }
    }
}
