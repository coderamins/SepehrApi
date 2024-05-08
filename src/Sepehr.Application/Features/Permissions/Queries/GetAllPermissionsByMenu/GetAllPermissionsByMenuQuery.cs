using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.Permissions.Queries.GetAllPermissionsByMenu;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.Permissions.Queries.GetAllPermissions
{
    public class GetAllPermissionsByMenuQuery : IRequest<Response<IEnumerable<PermissionsByMenuViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllPermissionByMenuQueryHandler :
         IRequestHandler<GetAllPermissionsByMenuQuery, Response<IEnumerable<PermissionsByMenuViewModel>>>
    {
        private readonly IPermissionRepositoryAsync _permissionRepository;
        private readonly IMapper _mapper;
        public GetAllPermissionByMenuQueryHandler(IPermissionRepositoryAsync permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<PermissionsByMenuViewModel>>> Handle(
            GetAllPermissionsByMenuQuery request, 
            CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetAllPermissionsByMenu();  
            var permissionViewModel = _mapper.Map<IEnumerable<PermissionsByMenuViewModel>>(permissions);

            return new Response<IEnumerable<PermissionsByMenuViewModel>>(permissionViewModel);
        }
    }
}