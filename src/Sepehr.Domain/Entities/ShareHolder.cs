using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class ShareHolder:AuditableBaseEntity<Guid>
    {
        public int ShareHolderCode { get; set; }
        [StringLength(150)]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(150)]
        public string LastName { get; set; } = string.Empty;
        [StringLength(150)]
        public string FatherName { get; set; } = string.Empty;
        [StringLength(11)]
        public string MobileNo { get; set; } = string.Empty;
    }
}
