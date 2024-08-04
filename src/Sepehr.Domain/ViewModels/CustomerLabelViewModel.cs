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
        public string LabelNameCode { get; set; } = string.Empty;
        public string LabelName { get; set; } = string.Empty;

    }
}
