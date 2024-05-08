using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class CustomerValidity:BaseEntity<int>
    {
        public required string ValidityDesc { get; set; }
        public string ColorCode { get; set; }
    }
}
