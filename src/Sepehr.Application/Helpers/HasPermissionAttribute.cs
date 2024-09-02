using Microsoft.AspNetCore.Authorization;

namespace Sepehr.Application.Helpers
{
    public sealed class HasPermissionAttribute:AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission)
            :base (policy:permission)
        {

        }

    }
}
