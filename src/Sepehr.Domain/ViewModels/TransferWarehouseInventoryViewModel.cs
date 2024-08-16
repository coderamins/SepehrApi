using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class TransferWarehouseInventoryViewModel
    {
        public Guid Id { get; set; }
        public int OriginWarehouseId { get; set; }
        public int OriginWarehouseDesc { get; set; }
        public int Amount { get; set; }
        public string CreatorName { get; set; }=string.Empty;
        public string CreatedDate { get; set; } = string.Empty;

        public List<TransferInventoryDetailViewModel> Details { get; set; } = new List<TransferInventoryDetailViewModel>();
    }              
}
