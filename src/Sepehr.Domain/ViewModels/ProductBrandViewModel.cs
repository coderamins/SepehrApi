using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class ProductBrandViewModel : BaseEntity<int>
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int BrandId { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }

        public ProductViewModel Product { get; set; }
        public BrandViewModel Brand { get; set; }
    }
}
