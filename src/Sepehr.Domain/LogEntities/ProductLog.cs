using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sepehr.Domain.Common;
using Sepehr.Domain.LogEntities;

namespace Sepehr.Domain.Entities
{
    public class ProductLog : AuditableBaseEntityLog<Guid>
    {
        public long ProductCode { get; set; }
        public string? Barcode { get; set; }
        public required string ProductName { get; set; }
        public int? ProductTypeId { get; set; }
        public required string ProductSize { get; set; }
        public string ProductThickness { get; set; } = string.Empty;
        public decimal ApproximateWeight { get; set; }
        public int NumberInPackage { get; set; }
        public int? ProductStandardId { get; set; }
        public int? ProductStateId { get; set; }
        [ForeignKey("ProductMainUnit")]
        public int ProductMainUnitId { get; set; }
        [ForeignKey("ProductSubUnit")]
        public int? ProductSubUnitId { get; set; }
        public double? ExchangeRate { get; set; }

        #region جهت اعلان های مختلف موجودی به کاربر 
        public int MaxInventory { get; set; }//---حداکثر موجودی
        public int MinInventory { get; set; }//---حداقل موجودی
        public int InventotyCriticalPoint { get; set; }//---نقطه بحرانی
        #endregion

        public string? Description { get; set; }

        public int LogTypeId { get; set; }
        public virtual LogType LogType { get; set; }


    }
}
