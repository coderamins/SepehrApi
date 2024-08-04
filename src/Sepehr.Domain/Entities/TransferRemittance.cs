using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class TransferRemittance:AuditableBaseEntity<int>
    {
        public int OriginWarehouseId { get; set; }
        public int DestinationWarehouseId { get; set; }
        public int TransferRemittanceTypeId { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public string ShippingName { get; set; } = string.Empty;
        public string Plaque { get; set; } = string.Empty;
        public int? VehicleTypeId { get; set; }
        public string DriverMobile { get; set; } = string.Empty;
        public string DriverAccountNo { get; set; } = string.Empty;
        public string? DriverCreditCardNo { get; set; } = string.Empty;
        public DateTime? DeliverDate { get; set; } 
        public decimal? FareAmount { get; set; }
        public bool FareAmountApproved { get; set; }
        public decimal? OtherCosts { get; set; }
        public int TransferRemittanceStatusId { get; set; } = 1;
        public string UnloadingPlaceAddress { get; set; } = string.Empty;
        public string? Description { get; set; }


        public virtual ICollection<TransferRemittanceDetail> Details { get; set; }=new List<TransferRemittanceDetail>();    
        public virtual required TransferRemittanceStatus TransferRemittanceStatus { get; set; }
        public virtual required VehicleType VehicleType { get; set; }
        public virtual required TransferRemittanceType TransferRemittanceType { get; set; }
        public virtual required Warehouse OriginWarehouse { get; set; }
        public virtual required Warehouse DestinationWarehouse { get; set; }
        public virtual EntrancePermit? EntrancePermit { get; set; }
    }
}
