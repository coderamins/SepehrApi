using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class UnloadingPermitDetailViewModel
    {
        public int Id { get; set; }
        public decimal UnloadedAmount { get; set; }
        public decimal SubUnitUnloadedAmount { get; set; }
        public TransferRemittanceDetailViewModel TransferRemittanceDetail { get; set; }

        public List<AttachmentViewModel> Attachments { get; set; } = new List<AttachmentViewModel>();

    }
}
