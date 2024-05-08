using Sepehr.Domain.Common;

namespace Sepehr.Domain.Entities.UserEntities
{
    public class UserRole :BaseEntity<Guid>
    {
        public required Guid UserId { get; set; }
        public required Guid RoleId { get; set; } 

        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
        public DateTime Created { get; set; }
    }
}
