using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class RentsViewModel
    {
        /// <summary>
        /// شماره خروج مجوز بارگیری یا شماره تخلیه بار
        /// </summary>
        public int ReferenceCode { get; set; }
        public Guid? LadingExitPermitId { get; set; }
        public Guid? PurchaseOrderTransferRemittanceUnloadingPermitId { get; set; }
        public string ReferenceDate { get; set; } = string.Empty;
        public int CargoAnnounceNo { get; set; }
        public string OrderTypeDesc { get; set; } = string.Empty;
        public decimal CargoTotalWeight { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public string DriverMobile { get; set; } = string.Empty;
        public string DriverAccountNo { get; set; } = string.Empty;
        public string AccountOwnerName { get; set; } = string.Empty;
        public decimal OtherCosts { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPayableAmount { get; set; }
        public decimal TotalLadingAmount { get; set; }
        public string CreatorName { get; set; } = string.Empty;
        //public RentsViewModel()
        //{
        //    TotalPayableAmount = TotalAmount + OtherCosts;
        //}

    }
}
