using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class CargoAnncDetailViewModel: BaseViewModel<int>
    {
        public Guid CargoAnnounceId { get; set; }
        public int OrderDetailId { get; set; }
        public decimal LadingAmount { get; set; }

        public decimal RealAmount { get; set; }
        public int PackageCount { get; set; }

        public OrderDetailViewModel OrderDetail { get; set; }
    }
}
