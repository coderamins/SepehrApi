using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class RentPaymentDetailViewModel
    {
        public string DriverName { get; set; } = string.Empty;
        public string DriverMobile { get; set; } = string.Empty;
        public int ReferenceCode { get; set; }
        public string OrderType { get; set; } = string.Empty;
        public string DriverAccountNo { get; set; } = string.Empty;

    }
}
