using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class EntrancePermitViewModel
    {
        public Guid Id { get; set; }
        public int PermitCode { get; set; }
        public string CreatorName { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;

        public TransferRemittanceViewModel TransferRemitance { get; set; }
        public List<AttachmentViewModel>? Attachments { get; set; }
        public IEnumerable<POTransRemittUnloadingPermitViewModel>? UnloadingPermits { get; set; }
    }
}
