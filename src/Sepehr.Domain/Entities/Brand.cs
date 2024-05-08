using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class Brand:BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public int Status { get; set; }

        public virtual ICollection<ProductBrand> ProductBrands { get; set; }
    }
}
