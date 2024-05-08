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

namespace Sepehr.Application.Features.Permissions.Queries.GetAllPermissions
{
    public class GetAllPermissionsQuery : IRequest<Response<IEnumerable<PermissionViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllPermissionQueryHandler :
         IRequestHandler<GetAllPermissionsQuery, Response<IEnumerable<PermissionViewModel>>>
    {
        private readonly IPermissionRepositoryAsync _permissionRepository;
        private readonly IMapper _mapper;
        public GetAllPermissionQueryHandler(IPermissionRepositoryAsync permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<PermissionViewModel>>> Handle(
            GetAllPermissionsQuery request, 
            CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllPermissionsParameter>(request);
            var permission = await _permissionRepository.GetAllPermissions();
              
            var permissionViewModel = _mapper.Map<IEnumerable<PermissionViewModel>>(permission);
            return new Response<IEnumerable<PermissionViewModel>>(permissionViewModel);
        }
    }
}