using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.WarehouseInventory
{
    public class OfficialWarehoseInventoryDto
    {
         public int productBrandId{get;set;}
         public int WarehouseId {get;set;}
         public int ApproximateInventory {get;set;}
         public int FloorInventory {get;set;}
         public int IsActive {get;set;}
    }
}
