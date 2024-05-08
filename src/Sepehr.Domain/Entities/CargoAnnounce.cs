using Sepehr.Domain.Common;
using Sepehr.Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class CargoAnnounce:AuditableBaseEntity<Guid>
    {
        public Guid OrderId { get; set; }
        public int CargoAnnounceNo { get; set; }
        public string UnloadingPlaceAddress { get; set; }= string.Empty;
        public string DriverName { get; set; }= string.Empty;
        public string CarPlaque { get; set; }= string.Empty;
        [StringLength(11)]
        public string DriverMobile { get; set; } = string.Empty;
        public Guid ApprovedUserId { get; set; }
        public DateTime ApprovedDate { get; set; }
        [DataType(DataType.Currency)]
        public decimal FareAmount { get; set; }
        /// <summary>
        /// تکمیل بارگیری- درصورتی که برای همه اقلام سفارش اعلام بار انجام شده باشد این گزینه TRUE خواهد شد
        /// </summary>
        public bool IsComplete { get; set; }
        public int VehicleTypeId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string ShippingName { get; set; } = string.Empty;
        public bool HasLadingPermit { get; set; }
        public string Description { get; set; }=string.Empty;

        public virtual required Order Order { get; set; }
        public virtual required VehicleType VehicleType { get; set; }
        //public required virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<LadingPermit> LadingPermits { get; set; } = new List<LadingPermit>();
        public virtual required ICollection<CargoAnnounceDetail> CargoAnnounceDetails { get; set; }
    }
}
