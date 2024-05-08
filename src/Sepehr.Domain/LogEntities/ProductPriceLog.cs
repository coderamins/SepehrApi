using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sepehr.Domain.Common;
using Sepehr.Domain.LogEntities;

namespace Sepehr.Domain.Entities
{
    public class ProductPriceLog : AuditableBaseEntity<Guid>
    {
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int ProductBrandId { get; set; }

        public int LogTypeId { get; set; }
        public virtual LogType LogType { get; set; }

    }
}
