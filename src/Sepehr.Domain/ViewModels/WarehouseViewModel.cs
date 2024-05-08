using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class WarehouseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WarehouseTypeId { get; set; }
        public Guid CustomerId { get; set; } = Guid.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string WarehouseTypeDesc { get; set; } = string.Empty;
    }
}
