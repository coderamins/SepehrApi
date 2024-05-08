using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class AttachmentViewModel
    {
        public Guid Id { get; set; }
        public byte[]? FileData { get; set; }
    }
}
