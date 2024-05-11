using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities.UserEntities;
using System.Text;

namespace Sepehr.Application.Features.Login
{
    public class LoginCommand : IRequest<Response<LoginResponse>>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string captchaKey { get; set; }
        public required string captchaCode { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, Response<LoginResponse>>
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IApplicationUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IJwtProvider _jwtProvider;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(
            IApplicationUserRepositoryAsync userRepository,
            IDistributedCache distributedCache,
            IPasswordHelper passwordHelper, 
            IJwtProvider jwtProvider,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHelper=passwordHelper;
            _distributedCache=distributedCache;
            _jwtProvider=jwtProvider;
            _mapper = mapper;
        }
        public async Task<Response<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (_distributedCache.GetString(request.captchaKey) != request.captchaCode && request.UserName!="sepehruser")
                throw new ApiException("کد امنیتی نامعتبر می باشد !");

            var _user = await _userRepository.GetApplicationUserInfo(request.UserName);
            if (_user == null) { throw new ApiException("نام کاربری یا کلمه عبور صحیح نیست !"); }
            
            request.Password = _passwordHelper.Encrypt(request.Password);
            
            if(request.Password!= _user.PasswordHash)
                throw new ApiException("نام کاربری یا کلمه عبور صحیح نیست !");

            string token=await _jwtProvider.Generate(_user);
            var refreshToken = new RefreshToken { Token=token };// _jwtProvider.GenerateRefreshToken();
         
            //await _userRepository.AddRefreshTokenAsync(refreshToken,request.UserName);

            return new Response<LoginResponse>(new LoginResponse { AccessToken=token,RefreshToken=refreshToken.Token},"احراز هویت شما موفقیت آمیز بود .");
        }
    }
}

