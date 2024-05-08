using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class LadingExitPermitDetailViewModel : BaseViewModel<Guid>
    {
        public Guid LadingExitPermitId { get; set; }
        public int CargoAnnounceDetailId { get; set; }
        public decimal LadingAmount { get; set; }
        public decimal RealAmount { get; set; }
        public int ProductSubUnitId { get; set; }
        public string ProductSubUnitDesc { get; set; } = string.Empty;
        public decimal ProductSubUnitAmount { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductBrandName { get; set; } = string.Empty;
        public string ProductMainUnitDesc { get; set; } = string.Empty;
        public int ProductCode { get; set; }

    }
}
