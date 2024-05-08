using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.ApplicationUsers.Queries.GetLoginedUserInfo
{
    public class GetLoginedUserInfoQuery : IRequest<Response<ApplicationUserViewModel>>
    {
        public class GetLoginedUserInfoQueryHandler : IRequestHandler<GetLoginedUserInfoQuery, Response<ApplicationUserViewModel>>
        {
            private readonly IApplicationUserRepositoryAsync _applicationUserRepository;
            private readonly IAuthenticatedUserService _userService;
            private readonly IMapper _mapper;

            public GetLoginedUserInfoQueryHandler(
                IApplicationUserRepositoryAsync applicationUserRepository,
                IAuthenticatedUserService userService,
                IMapper mapper
            )
            {
                _applicationUserRepository = applicationUserRepository;
                _userService = userService;
                _mapper = mapper;
            }

            public async Task<Response<ApplicationUserViewModel>>
            Handle(
                GetLoginedUserInfoQuery query,
                CancellationToken cancellationToken
            )
            {
                var applicationUser = await _applicationUserRepository.GetApplicationUserInfo(Guid.Parse(_userService.UserId));
                var applicationUserViewModel=_mapper.Map<ApplicationUserViewModel>(applicationUser);
                if (applicationUser == null)
                    throw new ApiException($"کاربر یافت نشد !");

                return new Response<ApplicationUserViewModel>(applicationUserViewModel);
            }
        }
    }
}
