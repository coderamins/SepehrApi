using Sepehr.Domain.Common;

namespace Sepehr.Domain.Entities
{
    public class DraftOrder:AuditableBaseEntity<int>
    {
        public bool Converted { get; set; } = false;
        public string Description { get; set; } = string.Empty;

        public ICollection<Attachment> Attachments { get; set; }
    }
}
