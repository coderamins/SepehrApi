using Sepehr.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sepehr.Domain.Entities.UserEntities
{
    public class RoleMenu : BaseEntity<Guid>
    {
        [ForeignKey("ApplicationRole")]
        public Guid ApplicationRoleId { get; set; }
        public Guid ApplicationMenuId { get; set; }

        public required virtual ApplicationMenu ApplicationMenu { get; set; }
        public required virtual ApplicationRole ApplicationRole { get; set; }
    }
}
