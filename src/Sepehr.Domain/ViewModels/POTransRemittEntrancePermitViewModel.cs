using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class POTransRemittEntrancePermitViewModel
    {
        public Guid Id { get; set; }
        public int PermitCode { get; set; }
        public List<AttachmentViewModel>? Attachments { get; set; }

        public IEnumerable<POTransRemittUnloadingPermitViewModel>? UnloadingPermits { get; set; }
    }
}
