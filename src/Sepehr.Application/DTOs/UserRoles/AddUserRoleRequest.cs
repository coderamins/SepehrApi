using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.UserRoles
{
    public class AddUserRoleRequest
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}
