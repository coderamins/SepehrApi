using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.ProductBrands.Queries.GetAllProductPricesByProductType
{
    public class GetAllProductPricesByProductTypeParameter : RequestParameter
    {
        public string? Keyword { get; set; }
        public int? ProductTypeId { get; set; }
    }
}