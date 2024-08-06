using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class CustomerLabel : AuditableBaseEntity<int>
    {
        public int CustomerLabelTypeId { get; set; }
        public string? LabelName { get; set; }
        public Guid? ProductId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? BrandId { get; set; }
        public int? ProductBrandId { get; set; }


        public virtual Product? Product { get; set; }
        public virtual ProductType? ProductType { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual ProductBrand? ProductBrand { get; set; }
        public virtual CustomerLabelType CustomerLabelType { get; set; }

    }
}
