using AutoMapper;
using Sepehr.Application.DTOs.Permission;
using Sepehr.Application.Helpers;
using Sepehr.Application.Interfaces;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.WebApi.Controller;
using System.Reflection;
using Swashbuckle.AspNetCore.Annotations;
using Azure;

namespace Sepehr.WebApi.Services
{
    public class PermissionDiscoveryService : IHostedService
    {
        private readonly IPermissionInitializerService _permissionInitializerService;
        private readonly IMapper _mapper;

        public PermissionDiscoveryService(IPermissionInitializerService permissionInitializerService, IMapper mapper)
        {
            _permissionInitializerService = permissionInitializerService;
            _mapper = mapper;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                List<PermissionDto> permissions = new List<PermissionDto>();
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
                            //var swagger_opt = method.GetCustomAttribute<ApiOperationAttribute>();

                            if (attribute != null)
                            {
                                if (!string.IsNullOrEmpty(attribute.Policy))
                                {
                                    if (!await _permissionInitializerService.CheckPermissionExists(attribute.Policy))
                                    {
                                        permissions.Add(new PermissionDto
                                        {
                                            Name = attribute.Policy
                                        });
                                    }
                                }
                            }
                        }
                    }
                }

                var newPermissions = _mapper.Map<List<Permission>>(permissions);
                await _permissionInitializerService.AddAsync(newPermissions);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
