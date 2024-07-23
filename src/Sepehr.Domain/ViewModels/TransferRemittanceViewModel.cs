using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class TransferRemittanceViewModel
    {
        public int Id { get; set; }
        public int TransferRemittanceStatusId { get; set; }
        public string TransferRemittanceStatusDesc { get; set; } = string.Empty;
        public int OriginWarehouseId { get; set; }
        public string OriginWarehouseName { get; set; } = string.Empty;
        public int DestinationWarehouseId { get; set; }
        public string DestinationWarehouseName { get; set; } = string.Empty;
        public int TransferRemittanceTypeId { get; set; }
        public string TransferRemittanceTypeDesc { get; set; } = string.Empty;
        public string DriverName { get; set; } = string.Empty;
        public string ShippingName { get; set; } = string.Empty;
        public string Plaque { get; set; } = string.Empty;
        public int VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; } = string.Empty;
        public string DriverMobile { get; set; } = string.Empty;
        public string DeliverDate { get; set; } = string.Empty;
        public decimal FareAmount { get; set; }
        public decimal? OtherCosts { get; set; }
        public Guid EntrancePermitId { get; set; }
        public int EntrancePermitCode { get; set; }
        public string UnloadingPlaceAddress { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string RegisterDate { get; set; } = string.Empty;
        public string EntrancePermitDate { get; set; } = string.Empty;
        public string DriverAccountNo { get; set; } = string.Empty;
        public string DriverCreditCardNo { get; set; } = string.Empty;
        public string CreatorName { get; set; } = string.Empty;

        public IEnumerable<TransferRemittanceDetailViewModel> Details { get; set; }
        public POTransRemittEntrancePermitViewModel EntrancePermit { get; set; }

    }
}
