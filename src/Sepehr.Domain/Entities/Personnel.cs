using Sepehr.Domain.Common;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class Personnel : AuditableBaseEntity<Guid>
    {
        public long PersonnelCode { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string FatherName { get; set; } = string.Empty;
        public string? OfficialName { get; set; }
        public string NationalId { get; set; } = string.Empty;
        public string NickName { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string? Address2 { get; set; }
        public string Email { get; set; } = string.Empty;

        public virtual ICollection<Phonebook> Phonebook { get; set; }=new List<Phonebook>();

    }
}
