using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.LadingPermit
{
    public class LadingExitPermitDetailDto
    {
        //public Guid? Id { get; set; }
        public int CargoAnnounceDetailId { get; set; }
        public decimal RealAmount { get; set; }
        public int? ProductSubUnitId { get; set; }
        public decimal? ProductSubUnitAmount { get; set; }
    }
}
