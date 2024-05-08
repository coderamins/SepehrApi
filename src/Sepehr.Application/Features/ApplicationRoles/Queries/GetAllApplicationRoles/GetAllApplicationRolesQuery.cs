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

namespace Sepehr.Application.Features.ApplicationRoles.Queries.GetAllApplicationRoles
{
    public class GetAllApplicationRolesQuery : IRequest<Response<IEnumerable<ApplicationRoleViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllApplicationRoleQueryHandler :
         IRequestHandler<GetAllApplicationRolesQuery, Response<IEnumerable<ApplicationRoleViewModel>>>
    {
        private readonly IApplicationRoleRepositoryAsync _applicationRoleRepository;
        private readonly IMapper _mapper;
        public GetAllApplicationRoleQueryHandler(IApplicationRoleRepositoryAsync applicationRoleRepository, IMapper mapper)
        {
            _applicationRoleRepository = applicationRoleRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ApplicationRoleViewModel>>> Handle(
            GetAllApplicationRolesQuery request, 
            CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllApplicationRolesParameter>(request);
            var applicationRole = await _applicationRoleRepository.GetAllApplicationRoles();
              
            var applicationRoleViewModel = _mapper.Map<IEnumerable<ApplicationRoleViewModel>>(applicationRole);
            return new Response<IEnumerable<ApplicationRoleViewModel>>(applicationRoleViewModel);
        }
    }
}