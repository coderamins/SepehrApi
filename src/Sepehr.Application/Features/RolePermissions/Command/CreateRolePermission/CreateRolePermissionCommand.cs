using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.RolePermissions.Command.CreateRolePermission
{
    public partial class CreateRolePermissionCommand : IRequest<Response<RolePermission>>
    {
        public required Guid PermissionId { get; set; }
        public required Guid RoleId { get; set; }

    }
    public class CreateRolePermissionCommandHandler : IRequestHandler<CreateRolePermissionCommand, Response<RolePermission>>
    {
        private readonly IRolePermissionRepositoryAsync _rolePermissionRepository;
        private readonly IMapper _mapper;
        public CreateRolePermissionCommandHandler(IRolePermissionRepositoryAsync rolePermissionRepository, IMapper mapper)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _mapper = mapper;
        }        

        public async Task<Response<RolePermission>> Handle(CreateRolePermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var rolePermission = _mapper.Map<RolePermission>(request);
                var checkDuplicate = await _rolePermissionRepository.GetRolePermissionInfo(request.RoleId,request.PermissionId);
                if (checkDuplicate != null) { throw new ApiException("نقش دسترسی با این مشخصات قبلا ایجاد است !"); }

                await _rolePermissionRepository.AddAsync(rolePermission);
                return new Response<RolePermission>(rolePermission, "نقش دسترسی جدید با موفقیت ایجاد گردید .");
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}