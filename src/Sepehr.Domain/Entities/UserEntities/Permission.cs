using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities.UserEntities
{
    public class Permission:BaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public Guid? ApplicationMenuId { get; set; }
        public string? Description { get; set; }
        public string MappedPermissions { get; set; } = string.Empty;

        public virtual ApplicationMenu? ApplicationMenu { get; set; }
    }
} 
