using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsParameter :RequestParameter
    {
        public ProductSortBase productSortBase { get; set; }
        public bool? ByBrand { get; set; }
        public int? WarehouseId { get; set; }
        public int? WarehouseTypeId { get; set; }
        public int? ProductTypeId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public long? OrderCode { get; set; }
        /// <summary>
        /// محصولاتی که در انبار، موجودی خرید دارند
        /// </summary>
        public bool? HasPurchaseInventory { get; set; }
    }
}