using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.ApplicationUsers.Queries.GetAllApplicationUsers
{
    public class GetAllApplicationUsersParameter : RequestParameter
    {
        public IEnumerable<string>? UserRoles { get; set; }
    }
}