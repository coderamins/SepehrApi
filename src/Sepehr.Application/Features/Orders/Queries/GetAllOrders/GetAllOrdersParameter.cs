using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersParameter : RequestParameter
    {
        public IEnumerable<int>? InvoiceTypeId { get; set; }
        public int? OrderStatusId { get; set; }
        public long? OrderCode { get; set; }
    }
}