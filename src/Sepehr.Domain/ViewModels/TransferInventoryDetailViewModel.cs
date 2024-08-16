using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class TransferInventoryDetailViewModel
    {
        public int Id { get; set; }
        public int TransferWarehouseInventoryId { get; set; }
        public int ProductBrandId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public decimal TransferAmount { get; set; }
    }
}
