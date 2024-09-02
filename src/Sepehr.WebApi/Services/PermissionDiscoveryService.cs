using Sepehr.Application.DTOs.Permission;
using Sepehr.Application.Helpers;
using Sepehr.Application.Interfaces;
using Sepehr.WebApi.Controller;
using System.Reflection;

namespace Sepehr.WebApi.Services
{
    public class PermissionDiscoveryService : IHostedService
    {
        private readonly IPermissionInitializerService _permissionInitializerService;

        public PermissionDiscoveryService(IPermissionInitializerService permissionInitializerService)
        {
            _permissionInitializerService = permissionInitializerService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes()
                    .Where(t => typeof(BaseApiController).IsAssignableFrom(t));

                foreach (var type in types)
                {
                    var methods = type.GetMethods();
                    foreach (var method in methods)
                    {
                        var attribute = method.GetCustomAttribute<HasPermissionAttribute>();

                        if (attribute != null)
                        {
                            //var permission = new Permission
                            //{
                            //    Name = attribute.Policy
                            //}; 
                            //_dbContext.Permissions.Add(permission);
                        }
                    }
                }
            }

           // await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
