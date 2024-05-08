using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class ProductDetail : BaseEntity<int>
    {
        public string? Size { get; set; }
        public string? Standard { get; set; }
        public string? ProductState { get; set; }

        public string? ProductIntegratedName
        {
            get
            {
                return string.Concat(Product.ProductName, " ", Size, " ", Standard, " ", ProductState);
            }
        }

        public Guid ProductId { get; set; }
        public required virtual Product Product { get; set; }
    }
}
