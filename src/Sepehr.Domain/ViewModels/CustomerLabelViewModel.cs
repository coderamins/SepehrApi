using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class CustomerLabelViewModel
    {
        public Guid CustomerId { get; set; }
        public int CustomerLabelTypeId { get; set; }
        public string CustomerLabelTypeDesc { get; set; } = string.Empty;
        public string? ProductName { get; set; } = string.Empty;
        public string? ProductTypeName { get; set; } = string.Empty;
        public string? BrandName { get; set; } = string.Empty;
        public string? ProductBrandName { get; set; } = string.Empty;

        public Guid? ProductId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? BrandId { get; set; }
        public int? ProductBrandId { get; set; }

        public string LabelName { get; set; } = string.Empty;

    }
}
