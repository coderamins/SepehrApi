using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Helpers;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Entities.UserEntities;

namespace Sepehr.Application.Features.Users.Command.ChangePassword
{
    public partial class ChangePasswordRequestCommand : IRequest<Response<bool>>
    {
        public required string UserName { get; set; }
        public required string NewPassword { get; set; }
        public required string VerificationCode { get; set; }
    }
    public class ChangePasswordRequestCommandHandler : IRequestHandler<ChangePasswordRequestCommand, Response<bool>>
    {
        private readonly IApplicationUserRepositoryAsync _applicationUserRepository;
        private readonly IUserRoleRepositoryAsync _userRoleRepository;
        private readonly ISmsService _smsService;
        private readonly IMapper _mapper;
        public ChangePasswordRequestCommandHandler(
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

        public async Task<Response<bool>> Handle(ChangePasswordRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userInfo = await _applicationUserRepository.GetApplicationUserInfo(request.UserName);
                if (userInfo == null) throw new ApiException("کاربر یافت نشد !");

                if (!await _applicationUserRepository.IsValidVerifyCode(request.UserName,request.VerificationCode))
                    throw new ApiException("کد تایید نامعتبر می باشد !");

                userInfo.PasswordHash = new PasswordHelper().Encrypt(request.NewPassword);
                await _applicationUserRepository.UpdateAsync(userInfo);
                await _applicationUserRepository.DeactivateVerifyCode(request.VerificationCode);

                return new Response<bool>(true, "کلمه عبور شما با موفقیت تغییر کرد .");
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }

}
