using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.ApplicationRole;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.ApplicationRoles.Command.CreateApplicationRole
{
    public partial class CreateApplicationRoleCommand : IRequest<Response<ApplicationRole>>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }

        public List<CreateRolePermissionDto> RolePermissions { get; set; } = new List<CreateRolePermissionDto>();

    }
    public class CreateApplicationRoleCommandHandler : IRequestHandler<CreateApplicationRoleCommand, Response<ApplicationRole>>
    {
        private readonly IApplicationRoleRepositoryAsync _applicationRoleRepository;
        private readonly IPermissionRepositoryAsync _permissionRepository;
        private readonly IMapper _mapper;
        public CreateApplicationRoleCommandHandler(
            IApplicationRoleRepositoryAsync applicationRoleRepository,
            IPermissionRepositoryAsync permissionRepository,
            IMapper mapper)
        {
            _applicationRoleRepository = applicationRoleRepository;
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<Response<ApplicationRole>> Handle(CreateApplicationRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var applicationRole = _mapper.Map<ApplicationRole>(request);
                var allRoles = await _applicationRoleRepository.GetAllAsync();
                if (allRoles.Any(r => r.Name == request.Name)) { throw new ApiException("نقش با این مشخصات قبلا ایجاد شده است !"); }

                foreach (var item in applicationRole.RolePermissions)
                {
                    if (await _permissionRepository.GetByIdAsync(item.PermissionId) == null)
                        throw new ApiException("دسترسی تخصیص داده شده یافت نشد !");
                }

                await _applicationRoleRepository.AddAsync(applicationRole);
                return new Response<ApplicationRole>(applicationRole, "نقش جدید با موفقیت ایجاد گردید .");
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}