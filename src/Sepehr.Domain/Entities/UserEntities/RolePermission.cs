using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities.UserEntities
{
    public class RolePermission:BaseEntity<Guid>
    {
        [ForeignKey("Permission")]
        public Guid PermissionId { get; set; }
        [ForeignKey("ApplicationRole")]
        public required Guid RoleId { get; set; }

        public virtual Permission Permission { get; set; } 
        public virtual ApplicationRole ApplicationRole { get; set; } 
    }
}
