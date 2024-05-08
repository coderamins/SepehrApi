using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sepehr.Application.DTOs.Account;
using Sepehr.Application.DTOs.Email;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Settings;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.DTOs.UserRoles;
using Sepehr.Domain.ViewModels;
using Sepehr.Domain.Entities.UserEntities;

namespace Sepehr.Infrastructure.Identity.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IdentityContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        public UserRoleService(IdentityContext dbContext, RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _roleManager = roleManager;
        }


        public async Task<Response<bool>> AddUserRole(AddUserRoleRequest request)
        {
            var checkExist = _dbContext.UserRoles.Where(r => r.RoleId == request.RoleId.ToString() && r.UserId == request.UserId.ToString());
            if (checkExist.Count()>0)
                throw new ApiException("This User Role has been already registered!");

            var result = await _dbContext.UserRoles.AddAsync(
                new UserRole 
                { 
                    UserId = request.UserId.ToString(), 
                    RoleId = request.RoleId.ToString() 
                });

            var r = await _dbContext.SaveChangesAsync();

            return new Response<bool>(true,"Role menu created successfuly .");
        }

        public async Task<Response<bool>> DeleteUserRole(AddUserRoleRequest request)
        {
            var userRole =await _dbContext.UserRoles
                .FirstOrDefaultAsync(r => r.RoleId == request.RoleId.ToString() && r.UserId == request.UserId.ToString());
            if (userRole == null)
                throw new ApiException("User Role not found!");

            _dbContext.Remove(userRole);
            var r = await _dbContext.SaveChangesAsync();

            return new Response<bool>(true, "User role deleted successfuly");
        }

        public async Task<Response<IEnumerable<UserRoleViewModel>>> GetAllUserRoles()
        {
            //var userRoles =await (from t in _dbContext.UserRoles
            //                join t1 in _dbContext.Roles on t.RoleId equals t1.Id
            //                select new UserRoleViewModel
            //                {
            //                    RoleName=t1.Name,
            //                    RoleDesc=t1.Description,
            //                    RoleId=Guid.Parse(t.RoleId),
            //                    UserId= Guid.Parse(t.UserId),
            //                }).ToListAsync();

            // return new Response<IEnumerable<UserRoleViewModel>>(userRoles);
            return null;
        }
    }
}
