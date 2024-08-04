using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllCustomerLabelsParameter : RequestParameter
    {
        public Guid CustomerId { get; set; } = Guid.Empty;
    }
}