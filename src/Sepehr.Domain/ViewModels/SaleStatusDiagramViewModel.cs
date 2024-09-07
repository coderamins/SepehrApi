using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class SaleStatusDiagramViewModel
    {
        public string SaleDate { get; set; } = string.Empty;
        public decimal OrderAmount { get; set; }
        public string ProductTypeDesc { get; set; } = string.Empty;
        public decimal Price { get; set; } 
    }
}
