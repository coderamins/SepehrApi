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

namespace Sepehr.Application.Features.Permissions.Command.UpdatePermission
{
    public class UpdatePermissionCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid ApplicationMenuId { get; set; }
        public string? Description { get; set; }


        public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IPermissionRepositoryAsync _permissionRepository;
            public UpdatePermissionCommandHandler(IPermissionRepositoryAsync permissionRepository, IMapper mapper)
            {
                _permissionRepository = permissionRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdatePermissionCommand command, CancellationToken cancellationToken)
            {
                var permission = await _permissionRepository.GetByIdAsync(command.Id);
                permission = _mapper.Map(command, permission);

                if (permission == null)
                {
                    throw new ApiException($"دسترسی یافت نشد !");
                }
                else
                {
                    await _permissionRepository.UpdateAsync(permission);
                    return new Response<string>(permission.Id.ToString(), "");
                }
            }
        }
    }
}