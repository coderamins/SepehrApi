using Sepehr.Domain.Common;
using Sepehr.Domain.LogEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class CargoAnnouncementLog:AuditableBaseEntityLog<Guid>
    {
        public Guid OrderId { get; set; }
        public required string UnloadingPlaceAddress { get; set; }
        public required string DriverName { get; set; }
        public required string CarPlaque { get; set; }
        [StringLength(11)]
        public required string DriverMobile { get; set; }
        public Guid ApprovedUserId { get; set; }
        public DateTime ApprovedDate { get; set; }
        [DataType(DataType.Currency)]
        public decimal RentAmount { get; set; }
        /// <summary>
        /// تکمیل بارگیری- درصورتی که برای همه اقلام سفارش اعلام بار انجام شده باشد این گزینه TRUE خواهد شد
        /// </summary>
        public bool IsComplete { get; set; }

        public int LogTypeId { get; set; }
        public virtual LogType LogType { get; set; }

    }
}
