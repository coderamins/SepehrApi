using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Login
{
    public class RefreshTokenCommand:IRequest<Response<LoginResponse>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Response<LoginResponse>>
    {
        private readonly IApplicationUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IJwtProvider _jwtProvider;

        public RefreshTokenCommandHandler(
            IApplicationUserRepositoryAsync userRepository,
            IPasswordHelper passwordHelper,
            IJwtProvider jwtProvider,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
        }
        public async Task<Response<LoginResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(request.AccessToken);
            var username = principal.Identity?.Name;

            var refreshToken = await _userRepository.GetSavedRefreshToken(username);
            if(refreshToken!=request.RefreshToken)
                throw new ApiException("احراز هویت انجام نشد !");

            var user=await _userRepository.GetApplicationUserInfo(username);

            var newJwtToken =await _jwtProvider.GenerateRefreshToken(user);
            if (newJwtToken == null)
                throw new ApiException("احراز هویت انجام نشد !");

            var newRefreshToken = _jwtProvider.GenerateRefreshToken();
            await _userRepository.AddRefreshTokenAsync(newRefreshToken, username);

            return new Response<LoginResponse>(new LoginResponse { AccessToken = newJwtToken, RefreshToken = newRefreshToken.Token }, "احراز هویت شما موفقیت آمیز بود .");
        }
    }

}
