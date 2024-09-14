using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.DraftOrders.Queries.GetAllDraftOrders
{
    public class GetAllDraftOrdersParameter :RequestParameter
    {
        public Guid? ProductId { get; set; }
        public bool? Converted { get; set; }
        public Guid? CreatorId { get; set; }
    }
}