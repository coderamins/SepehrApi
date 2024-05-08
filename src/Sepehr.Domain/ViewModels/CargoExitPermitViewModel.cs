using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class CargoExitPermitViewModel : BaseEntity<int>
    {
        public string ExitRegisterDate { get; set; } = string.Empty;
        public Guid ExitRegisterationUserId { get; set; }
        public string ExitRegisterationUserName { get; set; }= string.Empty;
        public required Guid CargoAnnounceId { get; set; }
        public required string DriverName { get; set; }
        public string FreightName { get; set; }   =string.Empty;
        public string PlaqueNo { get; set; }      =string.Empty;
        public string DriverMobileNo { get; set; } = string.Empty;
        public int VehicleTypeId { get; set; }
        public string VehicleTypeDesc { get; set; }=string.Empty;
        public string DeliverDate { get; set; } = string.Empty;
        public decimal FareAmount { get; set; }
        public string? UnloadingStationAddress { get; set; }

        public string? Description { get; set; }
    }
}
