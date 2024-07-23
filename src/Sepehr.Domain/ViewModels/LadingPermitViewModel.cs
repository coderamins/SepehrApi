using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class LadingPermitViewModel : BaseViewModel<int>
    {
        public required Guid CargoAnnounceId { get; set; }
        public bool? HasExitPermit { get; set; } = false;
        public string? Description { get; set; }
        public string CreatorName { get; set; } = string.Empty;

        public virtual CargoAnncViewModel CargoAnnounce { get; set; }

    }
}
