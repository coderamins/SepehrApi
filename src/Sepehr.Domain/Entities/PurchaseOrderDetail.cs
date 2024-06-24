using Sepehr.Domain.Common;
using Sepehr.Domain.Entities.BaseEntities;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class PurchaseOrderDetail: OrderDetailBaseEntity
    {
        public required virtual PurchaseOrder Order { get; set; }
        public virtual PurchaseInvoiceType? PurchaseInvoiceType { get; set; }
        public virtual Customer? PurchaserCustomer { get; set; }
        public required virtual ProductUnit ProductSubUnit { get; set; }
        public required virtual ProductBrand ProductBrand { get; set; }
        public virtual ProductBrand? AlternativeProductBrand { get; set; }
        public virtual ICollection<LadingPermitDetail>? LadingPermitDetails { get; set; }
    }
}   
