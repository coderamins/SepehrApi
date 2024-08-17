using Sepehr.Domain.Common;
using Sepehr.Domain.Entities.BaseEntities;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class PaymentRequest: PaymentRequestBase
    {
        public Guid CustomerId { get; set; }

        [JsonIgnore]
        public required virtual Customer Customer { get; set; }

    }
}
