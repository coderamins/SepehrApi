using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.ProductPrices.Queries.GetAllProductPrices
{
    public class GetAllProductPricesParameter :RequestParameter
    {
        public bool? IsActive { get; set; }
        public Guid? ProductId { get; set; }
    }
}