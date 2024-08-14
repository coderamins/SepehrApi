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
        public int ProductName { get; set; }
        public int BrandName { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
