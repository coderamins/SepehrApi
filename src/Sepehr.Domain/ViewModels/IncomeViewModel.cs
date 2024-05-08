using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class IncomeViewModel
    {
        public int Id { get; set; }
        public string IncomeDescription { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
