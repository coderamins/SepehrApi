using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities.UserEntities
{
    public class ApplicationMenu : AuditableBaseEntity<Guid>
    {
        public string AccessUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MenuIcon { get; set; } = string.Empty;
        [ForeignKey("ApplicationMenu")]
        public Guid? ApplicationMenuId { get; set; }

        public virtual ICollection<ApplicationMenu> Children { get; set; }=new List<ApplicationMenu>();
        public virtual ICollection<Permission> Permissions { get; set; }=new List<Permission>();
    }
}
