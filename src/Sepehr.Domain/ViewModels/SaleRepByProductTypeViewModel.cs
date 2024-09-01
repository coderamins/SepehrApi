using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class SaleRepByProductTypeViewModel
    {
        public string ProductTypeDesc { get; set; } = string.Empty;
        public decimal SaleAmount { get; set; }
    }
}
