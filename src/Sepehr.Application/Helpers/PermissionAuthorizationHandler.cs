using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Helpers
{
    public class PermissionAuthorizationHandler
        : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            PermissionRequirement requirement)
        {
            string? userId = context.User.Claims.FirstOrDefault(
                x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if(!Guid.TryParse(userId, out var parsedUserId)) {
                return;
            }

            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            IPermissionRepositoryAsync permissionRep = scope.ServiceProvider
                .GetRequiredService<IPermissionRepositoryAsync>();

            IUserRoleRepositoryAsync userRoleRep = scope.ServiceProvider
                .GetRequiredService<IUserRoleRepositoryAsync>();

            HashSet<string> permissions = await permissionRep
                .GetPermissionsAsync(parsedUserId);

            HashSet<string> userRoles = userRoleRep
                .GetAllUserRoles(userId);

            if (permissions.Contains(requirement.Permission) || userRoles.Contains("Admin"))
            {
                context.Succeed(requirement);
            }
        }
    }
}
