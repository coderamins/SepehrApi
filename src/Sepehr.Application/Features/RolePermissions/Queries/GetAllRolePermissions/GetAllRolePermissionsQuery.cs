using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.RolePermissions.Queries.GetAllRolePermissions
{
    public class GetAllRolePermissionsQuery : IRequest<Response<IEnumerable<RolePermissionViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllRolePermissionQueryHandler :
         IRequestHandler<GetAllRolePermissionsQuery, Response<IEnumerable<RolePermissionViewModel>>>
    {
        private readonly IRolePermissionRepositoryAsync _rolePermissionRepository;
        private readonly IMapper _mapper;
        public GetAllRolePermissionQueryHandler(IRolePermissionRepositoryAsync rolePermissionRepository, IMapper mapper)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<RolePermissionViewModel>>> Handle(
            GetAllRolePermissionsQuery request, 
            CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllRolePermissionsParameter>(request);
            var rolePermission = await _rolePermissionRepository.GetAllRolePermissions();
              
            var rolePermissionViewModel = _mapper.Map<IEnumerable<RolePermissionViewModel>>(rolePermission);
            return new Response<IEnumerable<RolePermissionViewModel>>(rolePermissionViewModel);
        }
    }
}