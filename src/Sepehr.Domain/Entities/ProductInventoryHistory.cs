using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class ProductInventoryHistory:AuditableBaseEntity<int>
    {
        public int productBrandId { get; set; }
        public int productCode           {get;set;}
        public int BrandId               {get;set;}
        public int ApproximateInventory  {get;set;}
        public string productName { get; set; } = string.Empty;
        public string BrandName             {get;set; } = string.Empty;
        public string priceDate { get; set; } = string.Empty;

    }
}
