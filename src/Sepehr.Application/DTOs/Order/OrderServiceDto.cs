using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.Order
{
    public class OrderServiceDto
    {
        public int? Id { get; set; } =null;
        public int ServiceId { get; set; }
        public string? Description { get; set; }
    }
}
