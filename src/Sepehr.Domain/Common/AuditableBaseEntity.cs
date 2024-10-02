using Sepehr.Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sepehr.Domain.Common
{
      public abstract class AuditableBaseEntity<T>: IAuditableBaseEntity<T> 
    {
        public virtual T Id { get; set; }
        [ForeignKey("ApplicationUser")]
        public Guid? CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}