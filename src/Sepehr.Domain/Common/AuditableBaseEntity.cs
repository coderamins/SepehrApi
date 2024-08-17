using Sepehr.Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sepehr.Domain.Common
{
      public abstract class AuditableBaseEntity<T> 
    {
        public virtual T Id { get; set; }
        [JsonIgnore]
        [ForeignKey("ApplicationUser")]
        public Guid? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime Created { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? LastModified { get; set; }
        [JsonIgnore]
        public bool IsActive { get; set; } = true;

        [JsonIgnore]
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}