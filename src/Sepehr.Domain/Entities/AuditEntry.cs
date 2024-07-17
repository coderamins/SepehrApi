using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }
        public EntityEntry Entry { get; }
        [ForeignKey("ApplicationUser")]
        public string? UserId { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object>? OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object>? NewValues { get; } = new Dictionary<string, object>();
        public AuditType AuditType { get; set; }
        public List<string> ChangedColumns { get; } = new List<string>();

        public virtual ApplicationUser? ApplicationUser { get; set; }
        public Audit ToAudit()
        {
            var audit = new Audit();
            audit.CreatedBy = Guid.Parse(UserId);
            audit.Type = AuditType.ToString();
            audit.TableName = TableName;
            audit.Created = DateTime.Now;
            audit.PrimaryKey = JsonConvert.SerializeObject(KeyValues);
            audit.OldValues = OldValues.Count == 0 ? "" : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? "" : JsonConvert.SerializeObject(NewValues);
            audit.AffectedColumns = ChangedColumns.Count == 0 ? "" : JsonConvert.SerializeObject(ChangedColumns);
            return audit;
        }
    }
}
