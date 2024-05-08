using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class ProductPriceViewModel:BaseViewModel<Guid>
    {
        public decimal Price { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public int ProductBrandId { get; set; }
        public string BrandName { get; set; } =string.Empty;
        public string RegisterDate { get; set; }=string.Empty;
        public long ProductCode { get; set; }
        public int Inventory { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = string.Empty;
        //public ProductViewModel ProductInfo { get; set; }

    }
}
