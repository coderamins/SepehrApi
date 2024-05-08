using Sepehr.Domain.Common;
using Sepehr.Domain.Enums;
using Sepehr.Domain.LogEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class OrderDetailLog:BaseEntityLog<int>
    {
        public Guid OrderId { get; set; }
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
        public required DateTime CargoSendDate { get; set; }

        #region برای مواردی که انبار واسط است
        public string? SellerCompanyRow { get; set; }
        [ForeignKey("Customer")]
        public Guid? PurchaserCustomerId { get; set; }
        [DataType(DataType.Currency)]
        public decimal PurchasePrice { get; set; }
        public int? PurchaseInvoiceTypeId { get; set; }
        public DateTime? PurchaseSettlementDate { get; set; }
        public string? Description { get; set; }
        #endregion
        
        #region برای مواردی که فاکتور رسمی بخواهد و از کالای اصلی موجودی رسمی نداشته باشیم
        [ForeignKey("AlternativeProduct")]
        public Guid? AlternativeProductId { get; set; }
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

        public int LogTypeId { get; set; }
        public virtual LogType LogType { get; set; }


    }
}   
