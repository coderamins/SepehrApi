using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.UserRoles.Queries.GetAllUserRoles
{
    public class GetAllUserRolesParameter 
    {
        public Guid UserId { get; set; } = Guid.Empty;
    }
}