using Sepehr.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class RoleMenuViewModel
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public Guid ApplicationMenuId { get; set; }
        public bool AccessStatus { get; set; }
        public List<ApplicationMenuNameViewModel> ApplicationMenuNames { get; set; }

    }
}
