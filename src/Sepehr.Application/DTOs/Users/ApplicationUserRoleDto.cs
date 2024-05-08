using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.Users
{
    public class ApplicationUserRoleDto
    {
        public required string UserId { get; set; }
        public required string RoleId { get; set; }
    }


}
