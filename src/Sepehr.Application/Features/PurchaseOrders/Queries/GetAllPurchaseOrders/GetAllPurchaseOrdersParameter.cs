using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.PurchaseOrders.Queries.GetAllPurchaseOrders
{
    public class GetAllPurchaseOrdersParameter : RequestParameter
    {
        public IEnumerable<int>? InvoiceTypeId { get; set; }
        public int? PurchaseOrderStatusId { get; set; }
        public bool? IsNotTransferedToWarehouse { get; set; }
        public long? OrderCode { get; set; }
    }
}