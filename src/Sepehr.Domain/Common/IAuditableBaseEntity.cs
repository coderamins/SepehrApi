using Sepehr.Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Common
{
    public interface IAuditableBaseEntity<T>
    {
        T Id { get; set; }
        [ForeignKey("ApplicationUser")]
        Guid? CreatedBy { get; set; }
        DateTime Created { get; set; }
        string? LastModifiedBy { get; set; }
        DateTime? LastModified { get; set; }
        bool IsActive { get; set; }

        ApplicationUser? ApplicationUser { get; set; }
    }
}
