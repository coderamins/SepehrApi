using Microsoft.AspNetCore.Identity;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Enums;
using Sepehr.Domain.Settings;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.DTOs.RoleMenu;
using System.Data;
using Sepehr.Domain.ViewModels;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Infrastructure.Persistence.Context;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;

namespace Sepehr.Infrastructure.Persistence
{
    public class RoleMenuService : IRoleMenuService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IMapper _mapper;
        public RoleMenuService(
            IAuthenticatedUserService authenticatedUserService,
            ApplicationDbContext dbContext,
            IMapper mapper)
        {
            _mapper = mapper;
            _authenticatedUserService = authenticatedUserService;
            _dbContext = dbContext;
        }


        public async Task<Response<bool>> AddRoleMenu(AddRoleMenuRequest request)
        {
            try
            {
                foreach (var m in request.ApplicationMenuId)
                {
                    var checkExist = await _dbContext.RoleMenus.FirstOrDefaultAsync(r => r.ApplicationRoleId == request.RoleId && r.ApplicationMenuId == m);

                    if (checkExist != null)
                        continue;

                    var roleMenuDto = new AddRoleMenuDto
                    {
                        ApplicationMenuId = m,
                        RoleId = request.RoleId,
                    };
                    var newRoleMenu = _mapper.Map<RoleMenu>(roleMenuDto);

                    var result = await _dbContext.RoleMenus.AddAsync(newRoleMenu);
                }

                await _dbContext.SaveChangesAsync();

                return new Response<bool>(true, "دسترسی نقش-منو با موفقیت ایجاد شد .");
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<Response<bool>> DeleteRoleMenu(IEnumerable<Guid> ids)
        {
            var rolemenus = _dbContext.RoleMenus.Where(m=> ids.Contains(m.Id)).AsQueryable();
            if (rolemenus.Count() <=0)
                throw new ApiException("نقش-منو یافت نشد !");

            foreach(var rm in  rolemenus)   
            _dbContext.RoleMenus.RemoveRange(rolemenus);
            await _dbContext.SaveChangesAsync();

            return new Response<bool>(true);
        }

        public async Task<Response<IEnumerable<RoleMenuViewModel>>> GetAllRoleMenus(Guid roleId)
        {
            try
            {
                var rolemenus = await _dbContext.RoleMenus
                       .Include(r => r.ApplicationMenu)
                       .Where(m => m.ApplicationRoleId == roleId)
                       .ToListAsync();


                if (rolemenus.Count() <= 0) throw new ApiException("Not found any record !");

                var result = _mapper.Map<IEnumerable<RoleMenuViewModel>>(rolemenus);

                return new Response<IEnumerable<RoleMenuViewModel>>(result);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<Response<IEnumerable<ApplicationMenuViewModel>>> GetUserApplicationMenus()
        {
            try
            {
                var userRoles = _authenticatedUserService.UserRoles;
                var roleIds = _dbContext.Roles.Where(r => userRoles.Contains(r.Name)).Select(x => x.Id).AsEnumerable();

                string uRoles = string.Join(',', userRoles.ToArray());
                var _roleMenus = _dbContext.RoleMenus.Where(x => roleIds.Contains(x.ApplicationRoleId)).Select(x => x.ApplicationMenuId).AsEnumerable();

                var appMenus =
                        _dbContext.ApplicationMenus.OrderBy(x =>x.OrderNo)
                        .Include(i => i.Children.Where(c => _roleMenus.Contains(c.Id) || userRoles.Contains("Admin")).OrderBy(x => x.OrderNo))
                        .ThenInclude(i => i.Children.Where(c => _roleMenus.Contains(c.Id) || userRoles.Contains("Admin")).OrderBy(x => x.OrderNo))
                        .ThenInclude(i => i.Children.Where(c => _roleMenus.Contains(c.Id) || userRoles.Contains("Admin")).OrderBy(x => x.OrderNo))
                        .ThenInclude(i => i.Children.Where(c => _roleMenus.Contains(c.Id) || userRoles.Contains("Admin")).OrderBy(x => x.OrderNo))
                        .ThenInclude(i => i.Children.Where(c => _roleMenus.Contains(c.Id) || userRoles.Contains("Admin")).OrderBy(x => x.OrderNo))
                        .Where(m => m.ApplicationMenuId == null)
                        .AsQueryable();

                var output = await appMenus
                    .Where(c => c.Children.Count() > 0 &&
                    _dbContext.ApplicationMenus.Where(a =>
                    _roleMenus.Contains(a.Id) || userRoles.Contains("Admin")).Select(m => m.ApplicationMenuId).Contains(c.Id))
                    .ToListAsync();

                if (output.Count() <= 0) throw new ApiException("رکوردی یافت  نشد !");

                var result = _mapper.Map<List<ApplicationMenuViewModel>>(output);
                return new Response<IEnumerable<ApplicationMenuViewModel>>(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Response<List<ApplicationMenuViewModel>>> GetAllApplicationMenus()
        {
            var appMenus =
                await _dbContext.ApplicationMenus.OrderBy(x => Convert.ToInt32(x.OrderNo))
                .Include(i => i.Children.OrderBy(x => x.OrderNo))
                .ThenInclude(c => c.Children.OrderBy(x => x.OrderNo))
                .ThenInclude(c => c.Children.OrderBy(x => x.OrderNo))
                .ThenInclude(c => c.Children.OrderBy(x => x.OrderNo))
                .Where(m => m.ApplicationMenuId == null)
                //.OrderBy(m => m.OrderNo)
                .ToListAsync();
            if (appMenus.Count() <= 0) throw new ApiException("رکوردی یافت  نشد !");

            var result = _mapper.Map<List<ApplicationMenuViewModel>>(appMenus);
            return new Response<List<ApplicationMenuViewModel>>(result);
        }

    }
}
