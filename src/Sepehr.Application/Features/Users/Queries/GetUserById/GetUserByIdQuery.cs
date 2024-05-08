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

namespace Sepehr.Application.Features.ApplicationUsers.Queries.GetApplicationUserById
{
    public class GetApplicationUserByIdQuery : IRequest<Response<ApplicationUserViewModel>>
    {
        public Guid Id { get; set; }

        public class GetApplicationUserByIdQueryHandler : IRequestHandler<GetApplicationUserByIdQuery, Response<ApplicationUserViewModel>>
        {
            private readonly IApplicationUserRepositoryAsync _applicationUserRepository;
            private readonly IMapper _mapper;

            public GetApplicationUserByIdQueryHandler(
                IApplicationUserRepositoryAsync applicationUserRepository,
                IMapper mapper
            )
            {
                _applicationUserRepository = applicationUserRepository;
                _mapper = mapper;
            }

            public async Task<Response<ApplicationUserViewModel>>
            Handle(
                GetApplicationUserByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var applicationUser = await _applicationUserRepository.GetApplicationUserInfo(query.Id);
                var applicationUserViewModel=_mapper.Map<ApplicationUserViewModel>(applicationUser);
                if (applicationUser == null)
                    throw new ApiException($"کاربر یافت نشد !");

                return new Response<ApplicationUserViewModel>(applicationUserViewModel);
            }
        }
    }
}
