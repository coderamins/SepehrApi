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

namespace Sepehr.Application.Features.Permissions.Queries.GetPermissionById
{
    public class GetPermissionByIdQuery : IRequest<Response<PermissionViewModel>>
    {
        public Guid Id { get; set; }

        public class GetPermissionByIdQueryHandler : IRequestHandler<GetPermissionByIdQuery, Response<PermissionViewModel>>
        {
            private readonly IPermissionRepositoryAsync _permissionRepository;
            private readonly IMapper _mapper;

            public GetPermissionByIdQueryHandler(
                IPermissionRepositoryAsync permissionRepository,
                IMapper mapper
            )
            {
                _permissionRepository = permissionRepository;
                _mapper = mapper;
            }

            public async Task<Response<PermissionViewModel>>
            Handle(
                GetPermissionByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var permission = await _permissionRepository.GetPermissionInfo(query.Id);
                var permissionViewModel=_mapper.Map<PermissionViewModel>(permission);
                if (permission == null)
                    throw new ApiException($"دسترسی یافت نشد !");

                return new Response<PermissionViewModel>(permissionViewModel);
            }
        }
    }
}
