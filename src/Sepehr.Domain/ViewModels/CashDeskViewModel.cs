using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class CashDeskViewModel
    {
        public int  Id { get; set; }
        public required string CashDeskDescription { get; set; }
        public bool IsActive { get; set; }

    }
}
