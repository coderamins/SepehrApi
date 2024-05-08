using Sepehr.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Permissions.Queries.GetAllPermissionsByMenu
{
    public class PermissionsByMenuViewModel
    {
        public Guid ApplicationMenuId { get; set; }
        public string ApplicationMenuName { get; set; } = string.Empty;
        public List<PermissionViewModel> Permissions { get; set; }

    }
}
