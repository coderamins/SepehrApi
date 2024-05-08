using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class OrderServiceViewModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string ServiceDesc { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
