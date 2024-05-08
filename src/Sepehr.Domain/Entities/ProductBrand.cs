using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class ProductBrand:BaseEntity<int>
    {
        public Guid ProductId { get; set; }
        public int BrandId { get; set;}

        public required virtual Brand Brand { get; set; }
        public required virtual Product Product { get; set; }
        public virtual ICollection<ProductPrice>? ProductPrices { get; set; }
        public virtual ICollection<ProductInventory>? ProductInventories { get; set; }

    }

}
