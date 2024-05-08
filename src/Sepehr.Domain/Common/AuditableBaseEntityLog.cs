using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Domain.Common
{
      public abstract class AuditableBaseEntityLog<T> 
    {
        public virtual T Id { get; set; }
        public virtual T MainId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public bool IsActive { get; set; } = true;
    }
}