using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class WarehouseType:BaseEntity<int>
    {
        public required string Description { get; set; }
    }
}
