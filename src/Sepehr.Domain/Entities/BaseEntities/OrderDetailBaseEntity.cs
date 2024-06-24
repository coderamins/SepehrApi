using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sepehr.Domain.Common;

namespace Sepehr.Domain.Entities.BaseEntities
{
    public class OrderDetailBaseEntity : AuditableBaseEntity<int>
    {
        public Guid OrderId { get; set; }
        public int RowId { get; set; }
        [ForeignKey("ProductBrand")]
        public required int ProductBrandId { get; set; }

        [DataType(DataType.Currency)]
        public decimal ProximateAmount { get; set; }
        public decimal TransferedAmount { get; set; }
        public int PackageNumber { get; set; }
        public int NumberInPackage { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public DateTime DeliverDate { get; set; }

        public string? Description { get; set; }

        #region برای مواردی که فاکتور رسمی بخواهد و از کالای اصلی موجودی رسمی نداشته باشیم
        [ForeignKey("AlternativeProductBrandId")]
        public int? AlternativeProductBrandId { get; set; }
        /// <summary>,
        /// مقدار کالای جایگزین
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal AlternativeProductAmount { get; set; }
        /// <summary>
        /// قیمت کالای جایگزین
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal AlternativeProductPrice { get; set; }
        #endregion

        #region واحد فرعی
        /// <summary>
        /// واحد فرعی
        /// </summary>
        public int? ProductSubUnitId { get; set; }

        /// <summary>
        /// مقدار واحد فرعی
        /// </summary>
        public decimal? ProductSubUnitAmount { get; set; }
        #endregion
    }
}
