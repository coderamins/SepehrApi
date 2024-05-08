using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.RolePermissions.Command.UpdateRolePermission
{
    public class UpdateRolePermissionCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }


        public class UpdateRolePermissionCommandHandler : IRequestHandler<UpdateRolePermissionCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IRolePermissionRepositoryAsync _rolePermissionRepository;
            public UpdateRolePermissionCommandHandler(IRolePermissionRepositoryAsync rolePermissionRepository, IMapper mapper)
            {
                _rolePermissionRepository = rolePermissionRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateRolePermissionCommand command, CancellationToken cancellationToken)
            {
                var rolePermission = await _rolePermissionRepository.GetByIdAsync(Guid.Parse(command.Id));
                rolePermission = _mapper.Map(command, rolePermission);

                if (rolePermission == null)
                {
                    throw new ApiException($"نقش دسترسی یافت نشد !");
                }
                else
                {
                    await _rolePermissionRepository.UpdateAsync(rolePermission);
                    return new Response<string>(rolePermission.Id.ToString(), "");
                }
            }
        }
    }
}