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
    public class OrderDetail : BaseEntity<int>
    {
        public Guid OrderId { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public int RowId { get; set; }
        public Guid ProductId { get; set; }
        public required int ProductBrandId { get; set; }
        public int WarehouseId { get; set; }
        [DataType(DataType.Currency)]
        public decimal ProximateAmount { get; set; }
        public int PackageNumber { get; set; }
        public int NumberInPackage { get; set; }
        //public int? ProductUnitId { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string? Description { get; set; }

        #region برای مواردی که انبار واسط است
        public string? SellerCompanyRow { get; set; }
        [ForeignKey("Customer")]
        public Guid? PurchaserCustomerId { get; set; }
        [DataType(DataType.Currency)]
        public decimal PurchasePrice { get; set; }
        public int? PurchaseInvoiceTypeId { get; set; }
        public DateTime? PurchaseSettlementDate { get; set; }
        #endregion

        #region برای مواردی که فاکتور رسمی بخواهد و از کالای اصلی موجودی رسمی نداشته باشیم
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

        public virtual PurchaseOrder? PurchaseOrder { get;set; }
        public required virtual Order Order { get; set; }
        public required virtual ProductUnit ProductSubUnit { get; set; }
        public required virtual ProductBrand ProductBrand { get; set; }
        public required virtual Product Product { get; set; }
        public required virtual Warehouse Warehouse { get; set; }
        public virtual InvoiceType? PurchaseInvoiceType { get; set; }
        public virtual Customer? PurchaserCustomer { get; set; }
        public virtual ProductBrand? AlternativeProductBrand { get; set; }
        public virtual ICollection<CargoAnnounceDetail>? CargoAnnounces { get; set; }
    }
}   
