using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class POrderTransRemittUnloadingPermitDetailViewModel
    {
        public int Id { get; set; }
        public decimal UnloadedAmount { get; set; }

        public List<AttachmentViewModel> Attachments { get; set; } = new List<AttachmentViewModel>();

    }
}
