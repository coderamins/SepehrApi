
using Sepehr.Domain.Common;

namespace Sepehr.Domain.Entities.UserEntities
{
    public class ApplicationRole:BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();


    }
}
