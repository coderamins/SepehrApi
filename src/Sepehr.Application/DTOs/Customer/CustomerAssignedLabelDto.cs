using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.Customer
{
    public class CustomerAssignedLabelDto
    {
        public Guid CustomerId { get; set; }
        public int LabelId { get; set; }
    }
}
