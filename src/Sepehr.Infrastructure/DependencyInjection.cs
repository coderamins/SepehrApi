using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sepehr.Application.Helpers;
using Sepehr.Application.Interfaces;
using Sepehr.Infrastructure.Authentication;

namespace Sepehr.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.AddTransient<IJwtProvider, JwtProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider,PermissionAuthorizationPolicyProvider>();    

            return services;
        }
    }
}