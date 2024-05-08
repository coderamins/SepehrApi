using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class LadingExitPermitViewModel: BaseViewModel<Guid>
    {
        public int LadingExitPermitCode { get; set; }
        public int LadingPermitId { get; set; }
        public string? BankAccountNo { get; set; }
        public string? CreditCardNo { get; set; }
        public string? BankAccountOwnerName { get; set; }
        public decimal? OtherAmount { get; set; }
        public string? ExitPermitDescription { get; set; }
        public decimal? FareAmount { get; set; }
        public bool? HasExitPermit { get; set; }

        public LadingPermitViewModel LadingPermit { get; set; }
        public List<LadingExitPermitDetailViewModel> LadingExitPermitDetails { get; set; }
    }
}
