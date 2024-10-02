using Sepehr.Domain.Common;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class CustomerOfficialCompany:AuditableBaseEntity<int>
    {
        public required string CompanyName { get; set; }
        public Guid CustomerId { get; set; }
        public string? EconomicId { get; set; }
        public string? PostalCode { get; set; }
        [StringLength(11)]
        public string? NationalId { get; set; }
        public CustomerType? CustomerType { get; set; }
        [StringLength(11)]
        public string? Tel1 { get; set; }
        [StringLength(11)]
        public string? Tel2 { get; set; }
        [StringLength(500)]
        public string? Address { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
