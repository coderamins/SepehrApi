using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.ProductBrands.Queries.GetAllProductBrands
{
    public class GetAllProductBrandsParameter :RequestParameter
    {
        public Guid? ProductId { get; set; }
    }
}