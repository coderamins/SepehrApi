using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class LadingLicenseViewModel : BaseEntity<int>
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
        public int ProductSubUnitId { get; set; }
        public string ProductSubUnitDesc { get; set; }
        public decimal ProductSubUnitAmount { get; set; }
        public bool HasExitPermit { get; set; } = false;

        public string? Description { get; set; }

        public string CreateDate { get; set; } = string.Empty;

        public virtual ProductUnit ProductSubUnit { get; set; }
        public virtual CargoAnncViewModel CargoAnnounce { get; set; }
        public virtual List<LadingLicenseDetailViewModel> LadingLicenseDetails { get; set; }

    }
}
