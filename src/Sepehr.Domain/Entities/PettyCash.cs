using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class PettyCash : BaseEntity<int>
    {
        [StringLength(11)]
        public string MobileNo { get; set; } = string.Empty;
        public string PettyCashDescription { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
