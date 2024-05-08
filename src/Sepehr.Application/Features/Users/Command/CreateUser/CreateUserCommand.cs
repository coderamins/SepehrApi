using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.Users;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Helpers;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities.UserEntities;

namespace Sepehr.Application.Features.ApplicationUsers.Command.CreateApplicationUser
{
    public partial class CreateApplicationUserCommand : IRequest<Response<ApplicationUser>>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }

        public List<CreateUserRoleDto> UserRoles { get; set; } = new List<CreateUserRoleDto>();
    }
    public class CreateApplicationUserCommandHandler : IRequestHandler<CreateApplicationUserCommand, Response<ApplicationUser>>
    {
        private readonly IApplicationUserRepositoryAsync _applicationUserRepository;
        private readonly IUserRoleRepositoryAsync _userRoleRepository;
        private readonly IMapper _mapper;
        public CreateApplicationUserCommandHandler(
            IApplicationUserRepositoryAsync applicationUserRepository,
            IUserRoleRepositoryAsync userRoleRepository,
            IMapper mapper)
        {
            _applicationUserRepository = applicationUserRepository;
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }        

        public async Task<Response<ApplicationUser>> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var applicationUser = _mapper.Map<ApplicationUser>(request);
                var checkDuplicate = await _applicationUserRepository.GetApplicationUserInfo(request.UserName);
                if (checkDuplicate != null) { throw new ApiException("کاربر با این مشخصات قبلا ایجاد است !"); }

                applicationUser.PasswordHash = new PasswordHelper().Encrypt(request.Password);

                //applicationUser.UserRoles.Clear();
                var newUser= await _applicationUserRepository.AddAsync(applicationUser);

                foreach (var uRole in request.UserRoles)
                {
                    await _userRoleRepository.AddAsync(new UserRole
                    {
                        RoleId= uRole.RoleId,
                        UserId=newUser.Id
                    });
                }

                return new Response<ApplicationUser>(applicationUser, "کاربر جدید با موفقیت ایجاد گردید .");
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}