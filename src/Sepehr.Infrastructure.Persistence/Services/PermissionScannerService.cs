using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Sepehr.Application.DTOs.Permission;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Infrastructure.Persistence.Context;
using System.Web.Http;

namespace Sepehr.Application.Helpers
{
    public class PermissionScannerService : IHostedService
    {
        private readonly IPermissionRepositoryAsync _permissionRepository;
        private readonly IMapper _mapper;

        public PermissionScannerService(
            //IPermissionRepositoryAsync permissionRepository,
            IMapper mapper)
        {
            //_permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
         {
            List<Permission> permissions = new List<Permission>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsSubclassOf(typeof(Controller)))
                    {
                        var methods = type.GetMethods();
                        foreach (var method in methods)
                        {
                            var attributes = method.GetCustomAttributes(typeof(HasPermissionAttribute), true);
                            foreach (HasPermissionAttribute attribute in attributes)
                            {
                                if (!string.IsNullOrEmpty(attribute.Policy))
                                {
                                    var permissionDto = new PermissionDto
                                    {
                                        Name = attribute.Policy,
                                        Description = ""
                                    };
                                    var newPermission = _mapper.Map<Permission>(permissionDto);
                                    permissions.Add(newPermission); 
                                }
                            }
                        }
                    }
                }
            }

            //await _permissionRepository.AddAsync(permissions);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
