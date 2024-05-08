using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.PurchaseOrder
{
    public class CreatePurchaseOrderDetailRequest
    {
        public int RowId { get; set; }
        //public Guid ProductId { get; set; }
        public decimal ProximateAmount { get; set; }
        public int NumberInPackage { get; set; }
        public decimal Price { get; set; }
        public required int ProductBrandId { get; set; }
        public int ProductSubUnitId { get; set; }
        public decimal ProductSubUnitAmount { get; set; }

        public string? Description { get; set; }

        public string? DeliverDate { get; set; } 
    }

    public class UpdatePurchaseOrderDetailRequest
    {
        public int Id { get; set; }
        public int RowId { get; set; }
        public decimal ProximateAmount { get; set; }
        public int NumberInPackage { get; set; }
        public decimal Price { get; set; }
        public decimal PurchasePrice { get; set; }
        public required int ProductBrandId { get; set; }
        public string? Description { get; set; }

        public string DeliverDate { get; set; }

    }

}
