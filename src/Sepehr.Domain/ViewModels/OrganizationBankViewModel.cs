using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class OrganizationBankViewModel
    {
        public int Id { get; set; }

        public required string AccountNo { get; set; }
        public required string AccountOwner { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public BankViewModel Bank { get; set; }
    }
}
