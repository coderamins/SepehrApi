using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.Warehouses.Queries.GetAllWarehouses
{
    public class GetAllWarehousesParameter :RequestParameter
    {
        public int warehouseId { get; set; }
        public int? WarehouseTypeId { get; set; }
        public Guid? CustomerId { get; set; }
    }
}