using System;
using System.Collections.Generic;
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
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.ApplicationRoles.Command.UpdateApplicationRole
{
    public class UpdateApplicationRoleCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public List<RolePermissionDto> RolePermissions { get; set; } = new List<RolePermissionDto>();

        public class UpdateApplicationRoleCommandHandler : IRequestHandler<UpdateApplicationRoleCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationRoleRepositoryAsync _applicationRoleRepository;
            public UpdateApplicationRoleCommandHandler(IApplicationRoleRepositoryAsync applicationRoleRepository, IMapper mapper)
            {
                _applicationRoleRepository = applicationRoleRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateApplicationRoleCommand command, CancellationToken cancellationToken)
            {
                var applicationRole = await _applicationRoleRepository.GetByIdAsync(command.Id);
                applicationRole = _mapper.Map(command, applicationRole);

                if (applicationRole == null)
                {
                    throw new ApiException($"نقش یافت نشد !");
                }
                else
                {
                    await _applicationRoleRepository.UpdateApplicationRoleAsync(applicationRole);
                    return new Response<string>(applicationRole.Id.ToString(), "");
                }
            }
        }
    }
}