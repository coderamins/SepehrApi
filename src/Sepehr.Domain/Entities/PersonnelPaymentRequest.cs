using Sepehr.Domain.Common;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Domain.Enums;

namespace Sepehr.Domain.Entities
{
    public class PersonnelPaymentRequest:AuditableBaseEntity<Guid>
    {
        public Guid PersonnelId { get; set; }

        public required virtual Personnel Personnel { get; set; }

    }
}
