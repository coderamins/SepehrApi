using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class OrganizationBank:BaseEntity<int>
    {
        public required int BankId { get; set; }
        public required string AccountNo { get; set; }
        public required string AccountOwner { get; set; }
        public string BranchName { get; set; } = string.Empty;

        public required virtual Bank Bank { get; set; }
    }
}
