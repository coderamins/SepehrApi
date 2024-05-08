using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class ApplicationMenuViewModel
    {
        public Guid Id { get; set; }
        public string AccessUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MenuIcon { get; set; } = string.Empty;
        public virtual List<ApplicationMenuViewModel>? Children { get; set; }

        public List<PermissionViewModel> Permissions { get; set; } = new List<PermissionViewModel>();
    }
}
