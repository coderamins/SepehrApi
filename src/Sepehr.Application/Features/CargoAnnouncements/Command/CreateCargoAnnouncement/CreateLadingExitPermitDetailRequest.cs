using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.CargoAnnouncements.Command.CreateCargoAnnouncement
{
    public class CreateLadingExitPermitDetailRequest
    {
        public int CargoAnnounceDetailId { get; set; }
        public int ProductSubUnitId { get; set; }
        public decimal ProductSubUnitAmount { get; set; }
        public decimal RealAmount { get; set; }
    }
}
