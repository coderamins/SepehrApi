using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class UnloadingPermitViewModel
    {
        public Guid Id { get; set; }
        public int UnloadingPermitCode { get; set; }
        public string UnloadingPlaceAddress { get; set; } = string.Empty;
        public string CreatorName { get; set; } = string.Empty;
        public List<AttachmentViewModel>? Attachments { get; set; }

        public List<UnloadingPermitDetailViewModel>? UnloadingPermitDetail { get; set; }
    }
}
