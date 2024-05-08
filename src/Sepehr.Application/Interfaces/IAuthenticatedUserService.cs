using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        List<string> UserRoles { get; }
    }
}
