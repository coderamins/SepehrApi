using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class CargoAnncViewModel: BaseViewModel<Guid>
    {
        public int CargoAnnounceNo { get; set; }
        public Guid OrderId { get; set; }
        public required string UnloadingPlaceAddress { get; set; }
        public required string DriverName { get; set; }
        public required string CarPlaque { get; set; }
        public required string DriverMobile { get; set; }
        public string ApprovedUserName { get; set; } = string.Empty;
        public string ApprovedDate { get; set; }=string.Empty;
        public decimal FareAmount { get; set; }
        public string ApprovedUserId { get; set; }=string.Empty;
        public bool IsComplete { get; set; }
        public int VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; }=string.Empty;
        public string DeliveryDate { get; set; }=string.Empty;
        public string Description { get; set; }=string.Empty;
        public string ShippingName { get; set; }=string.Empty;
        public bool HasExitPermit { get; set; }

        public CargoAnncOrderViewModel Order { get; set; }
        public LadingPermitViewModel LadingPermit { get; set; }
        public List<CargoAnncDetailViewModel> CargoAnnounceDetails { get; set; }
    }
}
