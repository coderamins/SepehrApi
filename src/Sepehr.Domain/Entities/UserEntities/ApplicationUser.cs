using Sepehr.Domain.Common;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Sepehr.Domain.Entities.UserEntities
{
    public class ApplicationUser:BaseEntity<Guid>
    {
        public ApplicationUser()
        {
            this.Roles = new Collection<UserRole>();
        }
        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }

        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [StringLength(11)]
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsDealerUser { get; set; } = false;

        public Guid? CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }
        public virtual ICollection<UserRole> Roles {get;set;}

        //public virtual ICollection<ApplicationRole> Roles {get;set;}

    }
}