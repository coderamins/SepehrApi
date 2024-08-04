using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class TransferRemittanceDetail:BaseEntity<int>
    {
        public int ProductBrandId { get; set; }
        public decimal TransferAmount { get; set; }
        public int TransferRemittanceId { get; set; }

        public virtual required TransferRemittance TransferRemittance { get; set; }
        public virtual ICollection<UnloadingPermitDetail>? UnloadingPermitDetail { get; set; }
        public virtual required ProductBrand ProductBrand { get; set; }
    }
}
