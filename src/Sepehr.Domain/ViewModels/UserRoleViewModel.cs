using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class UserRoleViewModel
    {
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }

        public ApplicationRoleViewModel Role { get; set; }=new ApplicationRoleViewModel();
    }
}
