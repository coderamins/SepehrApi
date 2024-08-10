using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class UnloadingPermitViewModel
    {
        public Guid Id { get; set; }
        public int UnloadingPermitCode { get; set; }
        public string CreatorName { get; set; } = string.Empty;
        public Guid EntrancePermitId { get; set; }
        public string DriverAccountNo { get; set; } = string.Empty;
        public string DriverCreditCardNo { get; set; } = string.Empty;
        public decimal OtherCosts { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public decimal FareAmount { get; set; }
        public bool FareAmountApproved { get; set; }
        public bool FareAmountPayStatus { get; set; } = false;
        public string ShippingName { get; set; } = string.Empty;
        public string Plaque { get; set; } = string.Empty;
        public int VehicleTypeId { get; set; }
        public string DriverMobile { get; set; } = string.Empty;
        public string DeliverDate { get; set; } = string.Empty;
        public string UnloadingPlaceAddress { get; set; } = string.Empty;

        public List<AttachmentViewModel>? Attachments { get; set; }

        public List<UnloadingPermitDetailViewModel>? UnloadingPermitDetails { get; set; }
    }
}
