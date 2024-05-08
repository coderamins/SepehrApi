using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Sepehr.Application.Interfaces;

namespace Sepehr.WebApi.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId= httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            var userIdentity = (ClaimsIdentity)httpContextAccessor.HttpContext?.User?.Identity;
            if (userIdentity != null)
            {
                var roleClaimType = userIdentity.RoleClaimType;
                var roles = httpContextAccessor.HttpContext?.User?.Claims.Where(x => x.Type == roleClaimType).ToList();
                foreach (var role in roles)
                {
                    UserRoles.Add(role.Value);
                }
            }
        }
        public string UserId {get;}
        public List<string>? UserRoles { get; } = new List<string>();
    }
}