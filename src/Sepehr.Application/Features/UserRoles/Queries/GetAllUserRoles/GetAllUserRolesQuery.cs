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

namespace Sepehr.Application.Features.UserRoles.Queries.GetAllUserRoles
{
    public class GetAllUserRolesQuery : IRequest<Response<List<UserRoleViewModel>>>
    {
        public Guid UserId { get; set; } = Guid.Empty;
    }
    public class GetAllUserRoleQueryHandler :
         IRequestHandler<GetAllUserRolesQuery, Response<List<UserRoleViewModel>>>
    {
        private readonly IUserRoleRepositoryAsync _userRoleRepository;
        private readonly IMapper _mapper;
        public GetAllUserRoleQueryHandler(IUserRoleRepositoryAsync userRoleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<UserRoleViewModel>>> Handle(
            GetAllUserRolesQuery request, 
            CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllUserRolesParameter>(request);
            var userRole = await _userRoleRepository.GetAllUserRoles(request.UserId);
              
            var userRoleViewModel = _mapper.Map<List<UserRoleViewModel>>(userRole);
            return new Response<List<UserRoleViewModel>>(userRoleViewModel);
        }
    }
}