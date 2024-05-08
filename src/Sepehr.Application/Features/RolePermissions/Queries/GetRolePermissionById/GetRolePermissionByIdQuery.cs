using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.RolePermissions.Queries.GetRolePermissionById
{
    public class GetRolePermissionByIdQuery : IRequest<Response<RolePermissionViewModel>>
    {
        public Guid Id { get; set; }

        public class GetRolePermissionByIdQueryHandler : IRequestHandler<GetRolePermissionByIdQuery, Response<RolePermissionViewModel>>
        {
            private readonly IRolePermissionRepositoryAsync _rolePermissionRepository;
            private readonly IMapper _mapper;

            public GetRolePermissionByIdQueryHandler(
                IRolePermissionRepositoryAsync rolePermissionRepository,
                IMapper mapper
            )
            {
                _rolePermissionRepository = rolePermissionRepository;
                _mapper = mapper;
            }

            public async Task<Response<RolePermissionViewModel>>
            Handle(
                GetRolePermissionByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var rolePermission = await _rolePermissionRepository.GetRolePermissionInfo(query.Id);
                var rolePermissionViewModel=_mapper.Map<RolePermissionViewModel>(rolePermission);
                if (rolePermission == null)
                    throw new ApiException($"نقش دسترسی یافت نشد !");

                return new Response<RolePermissionViewModel>(rolePermissionViewModel);
            }
        }
    }
}
