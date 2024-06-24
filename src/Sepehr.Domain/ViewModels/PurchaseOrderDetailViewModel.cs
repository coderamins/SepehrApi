using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class PurchaseOrderDetailViewModel
    {
        public int Id { get; set; }
        public int RowId { get; set; }
        public string? ProductName { get; set; }
        public string BrandName { get; set; }
        public string? WarehouseName { get; set; }
        public decimal ProximateAmount { get; set; }
        public int NumberInPackage { get; set; }
        public decimal Price { get; set; }
        public decimal PurchasePrice { get; set; }
        public int ProductBrandId { get; set; }
        public int ProductSubUnitId { get; set; }
        public string ProductSubUnitDesc { get; set; }=string.Empty;
        public decimal ProductSubUnitAmount { get; set; }

        public string DeliverDate { get; set; }

        public Guid? AlternativeProductBrandId { get; set; }
        public string AlternativeProductBrandName { get; set; }
        public decimal AlternativeProductAmount { get; set; }
        public decimal AlternativeProductPrice { get; set; }
        public string? Description { get; set; }
        public int BrandId { get; set; }


        public ProductBrandViewModel ProductBrand { get; set; }
        public ProductViewModel Product { get; set; }
        public CustomerViewModel? PurchaserCustomer { get; set; }
    }
}
