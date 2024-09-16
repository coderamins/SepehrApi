using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.Sms;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Helpers;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.Users.Command.ForgetPassword
{
    public partial class ForgetPasswordRequestCommand : IRequest<Response<bool>>
    {
        public string UserName { get; set; }
    }
    public class ForgetPasswordRequestCommandHandler : IRequestHandler<ForgetPasswordRequestCommand, Response<bool>>
    {
        private readonly IApplicationUserRepositoryAsync _applicationUserRepository;
        private readonly IUserRoleRepositoryAsync _userRoleRepository;
        private readonly ISmsService _smsService;
        private readonly IMapper _mapper;
        public ForgetPasswordRequestCommandHandler(
            IApplicationUserRepositoryAsync applicationUserRepository,
            IUserRoleRepositoryAsync userRoleRepository,
            IMapper mapper,
            ISmsService smsService)
        {
            _applicationUserRepository = applicationUserRepository;
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
            _smsService = smsService;
        }

        public async Task<Response<bool>> Handle(ForgetPasswordRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userInfo = await _applicationUserRepository.GetApplicationUserInfo(request.UserName);
                if (userInfo == null) throw new ApiException("کاربر یافت نشد !");

                if(string.IsNullOrEmpty(userInfo.Mobile))
                    throw new ApiException("شماره موبایل نامعتبر می باشد !");

                if (await _applicationUserRepository.HasAnyActiveVerifyCode(request.UserName))
                    throw new ApiException("کد تایید قبلا برای شما ارسال شده است !");

                string verifyCode = "";
                await _smsService.SendVerifyCode(userInfo.Mobile, VerificationCodeHelper.GenerateVerificationCode(out verifyCode, 5));

                await _applicationUserRepository.CreateVerificationCode(new VerificationCode
                {
                    Code = verifyCode,
                    UserName=userInfo.UserName,
                    IsUsed=false,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                });

                return new Response<bool>(true, "کد فراموشی با موفقیت ارسال شد .");
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }

}
