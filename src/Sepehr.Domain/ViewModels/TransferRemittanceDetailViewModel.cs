using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class TransferRemittanceDetailViewModel
    {
        public int Id { get; set; }
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public int ProductBrandId { get; set; }
        public string BrandName { get; set; }
        public decimal TransferAmount { get; set; }
        public decimal UnloadedAmount { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
