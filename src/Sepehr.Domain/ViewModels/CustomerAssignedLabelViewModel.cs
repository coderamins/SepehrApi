using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class CustomerAssignedLabelViewModel
    {
        public int CustomerLabelId { get; set; }
        public Guid CustomerId { get; set; }

        public string CustomerLabelName { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductTypeName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public string ProductBrandId { get; set; }  = string.Empty;
    }
}
