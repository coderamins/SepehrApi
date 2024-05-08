using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sepehr.Domain.Common;

namespace Sepehr.Domain.Entities
{
    public class ProductPrice : AuditableBaseEntity<Guid>
    {
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int ProductBrandId { get; set; }

        public virtual ProductBrand ProductBrand {get;set;}
    }
}
