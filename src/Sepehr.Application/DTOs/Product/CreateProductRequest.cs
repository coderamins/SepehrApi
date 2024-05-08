using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.Product
{
    public class CreateProductRequest
    {
        public required string ProductName { get; set; }
        public string? ProductCode { get; set; }
        public int BrandId { get; set; }
        public required string ProductSize { get; set; }
        public decimal ApproximateWeight { get; set; }
        public int NumberInPackage { get; set; }
        public int StatusId { get; set; }

        public required Brand ProductBrand { get; set; }
        public ProductDetail? ProductDetail { get; set; }

    }
}
