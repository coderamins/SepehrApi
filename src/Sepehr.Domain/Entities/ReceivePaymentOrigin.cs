using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class ReceivePaymentOrigin:BaseEntity<int>
    {
        public string Desc { get; set; } = string.Empty;
    }
}
