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

namespace Sepehr.Application.Features.ApplicationUsers.Queries.GetAllApplicationUsers
{
    public class GetAllApplicationUsersQuery : IRequest<PagedResponse<IEnumerable<ApplicationUserViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllApplicationUserQueryHandler :
         IRequestHandler<GetAllApplicationUsersQuery, PagedResponse<IEnumerable<ApplicationUserViewModel>>>
    {
        private readonly IApplicationUserRepositoryAsync _applicationUserRepository;
        private readonly IMapper _mapper;
        public GetAllApplicationUserQueryHandler(IApplicationUserRepositoryAsync applicationUserRepository, IMapper mapper)
        {
            _applicationUserRepository = applicationUserRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ApplicationUserViewModel>>> Handle(
            GetAllApplicationUsersQuery request, 
            CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllApplicationUsersParameter>(request);
            var applicationUser = await _applicationUserRepository.GetAllApplicationUsers();
              
            var applicationUserViewModel = _mapper.Map<IEnumerable<ApplicationUserViewModel>>(applicationUser);
            return new PagedResponse<IEnumerable<ApplicationUserViewModel>>(
                applicationUserViewModel, 
                validFilter.PageNumber, 
                validFilter.PageSize);
        }
    }
}