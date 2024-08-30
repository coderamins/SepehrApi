using Sepehr.Domain.Common;
using Sepehr.Domain.Entities.BaseEntities;

namespace Sepehr.Domain.Entities
{
    public class PaymentRequest: PaymentRequestBase
    {
        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

    }
}
