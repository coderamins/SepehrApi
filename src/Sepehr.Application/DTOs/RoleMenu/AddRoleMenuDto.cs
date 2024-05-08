using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.RoleMenu
{
    public class AddRoleMenuDto
    {
        public Guid RoleId { get; set; }
        public Guid ApplicationMenuId { get; set; }

    }
}
