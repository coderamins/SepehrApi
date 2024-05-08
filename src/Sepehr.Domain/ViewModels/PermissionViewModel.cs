using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class PermissionViewModel
    {
        public Guid Id { get; set; }
        public string PermissionName { get; set; } = string.Empty;
        public Guid ApplicationMenuId { get; set; }
        public string ApplicationMenuName { get; set; } = string.Empty;
        public string? Description { get; set; }

    }
}
