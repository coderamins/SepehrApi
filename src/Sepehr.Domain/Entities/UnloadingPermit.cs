﻿using Sepehr.Domain.Common;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class UnloadingPermit : AuditableBaseEntity<Guid>
    {
        public Guid EntrancePermitId { get; set; }
        public int UnloadingPermitCode { get; set; }
        public string DriverAccountNo { get; set; } = string.Empty;
        public string DriverCreditCardNo { get; set; } = string.Empty;
        public decimal OtherCosts { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public decimal FareAmount { get; set; }
        public string ShippingName { get; set; } = string.Empty;
        public string Plaque { get; set; } = string.Empty;
        public int VehicleTypeId { get; set; }
        public string DriverMobile { get; set; } = string.Empty;
        public string DeliverDate { get; set; } = string.Empty;
        public string UnloadingPlaceAddress { get; set; } = string.Empty;
        public int? FareAmountStatusId { get; set; }=(int)EFareAmountStatus.InProgress;

        //public ICollection<DriverFareAmountApprove>? DriverFareAmountApproves { get; set; }
        public virtual FareAmountStatus? FareAmountStatus { get; set; }
        public virtual ICollection<RentPayment> RentPayments { get; set; } =new List<RentPayment>();
        public virtual required EntrancePermit EntrancePermit { get; set; }
        public virtual ICollection<Attachment>? Attachments { get; set; }
        public virtual required ICollection<UnloadingPermitDetail> UnloadingPermitDetails { get; set; }
    }
}
