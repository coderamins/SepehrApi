using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class Cost:BaseEntity<int>
    {
        [StringLength(200)]
        public required string CostDescription { get; set; }
    }
}
