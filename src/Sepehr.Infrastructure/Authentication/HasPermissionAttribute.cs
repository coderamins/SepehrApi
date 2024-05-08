using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Infrastructure.Authentication
{
    public sealed class HasPermissionAttribute:AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission)
            :base (policy:permission)
        {

        }

    }
}
