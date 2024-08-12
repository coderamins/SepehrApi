using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.TransferWarehouseInventories.Queries.GetAllTransferWarehouseInventories
{
    public class GetAllTransferWarehouseInventoriesParameter : RequestParameter
    {
        public int? OriginWarehouseId { get; set; }

    }
}