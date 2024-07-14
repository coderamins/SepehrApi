using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class Audit:AuditableBaseEntity<int>
    {
        public string Type { get; set; } = string.Empty;
        public string TableName { get; set; }=string.Empty;
        public string OldValues { get; set; }=string.Empty;
        public string NewValues { get; set; }=string.Empty;
        public string AffectedColumns { get; set; }=string.Empty;
        public string PrimaryKey { get; set; }=string.Empty;
    }
}
