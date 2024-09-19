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

        public async Task<Response<bool>> DeleteRoleMenu(Guid id)
        {
            var rolemenu = await _dbContext.RoleMenus.FindAsync(id);
            if (rolemenu == null)
                throw new ApiException("نقش-منو یافت نشد !");

            _dbContext.RoleMenus.Remove(rolemenu);
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

            var role = _dbContext.Roles
                .Include(x=>x.RoleMenus)
                .FirstOrDefault(r => r.Id.ToString() == "d3a2ee97-7825-40cc-f6a3-08dcd7b40b61");
            if (role == null)
            {
                throw new ArgumentException("نقش معتبر نیست.");
            }

            // گرفتن شناسه منوی‌های مرتبط با نقش
            var roleMenuIds = role.RoleMenus.Select(rm => rm.ApplicationMenuId).ToList();
            var test1 = BuildMenuHierarchy(roleMenuIds);


            var userRoles = _authenticatedUserService.UserRoles;
            var roleIds = _dbContext.Roles.Where(r => userRoles.Contains(r.Name)).Select(x => x.Id).AsEnumerable();

            string uRoles = string.Join(',', userRoles.ToArray());
            var _roleMenus = _dbContext.RoleMenus
                .Where(x => roleIds.Contains(x.ApplicationRoleId)).Select(x => x.Id).ToList();

            var test= BuildMenuHierarchy(_roleMenus);







            var hasAccessMenus = await
                    _dbContext.ApplicationMenus
                    .Where(x => _roleMenus.Contains(x.Id)).ToListAsync();

            List<Guid> parents = new List<Guid>();
            parents.AddRange(hasAccessMenus.Where(x => x.ApplicationMenuId != null).Select(x => x.Id));

            bool parentStatus = true;
            while (hasAccessMenus != null)
            {
                hasAccessMenus = await _dbContext.ApplicationMenus
                    .Where(m => hasAccessMenus.Where(x => x.ApplicationMenuId != null).Select(x => x.Id).Contains(m.Id)).ToListAsync();

                if (hasAccessMenus != null)
                    parents.AddRange(hasAccessMenus.Where(x => x.ApplicationMenuId == null).Select(x => x.Id));
            }

            var pm = _dbContext.ApplicationMenus
                .Include(x => x.Children.Where(c => _roleMenus.Contains(c.Id)))
                .ThenInclude(x => x.Children.Where(c => _roleMenus.Contains(c.Id)))
                .ThenInclude(x => x.Children.Where(c => _roleMenus.Contains(c.Id)))
                .ThenInclude(x => x.Children.Where(c => _roleMenus.Contains(c.Id)))
                .Where(x => x.ApplicationMenuId == null);
            var menus = await pm
                .Include(i => i.Children.OrderBy(x => x.OrderNo).Where(c => _roleMenus.Contains(c.Id) || (c.ApplicationMenuId != null && _roleMenus.Contains((Guid)c.ApplicationMenuId)) || userRoles.Contains("Admin")).OrderBy(x => x.OrderNo))
                .ThenInclude(i => i.Children.OrderBy(x => x.OrderNo).Where(c => _roleMenus.Contains(c.Id) || (c.ApplicationMenuId != null && _roleMenus.Contains((Guid)c.ApplicationMenuId)) || userRoles.Contains("Admin")).OrderBy(x => x.OrderNo))
                .ThenInclude(i => i.Children.OrderBy(x => x.OrderNo).Where(c => _roleMenus.Contains(c.Id) || (c.ApplicationMenuId != null && _roleMenus.Contains((Guid)c.ApplicationMenuId)) || userRoles.Contains("Admin")).OrderBy(x => x.OrderNo))
                .ThenInclude(i => i.Children.OrderBy(x => x.OrderNo).Where(c => _roleMenus.Contains(c.Id) || (c.ApplicationMenuId != null && _roleMenus.Contains((Guid)c.ApplicationMenuId)) || userRoles.Contains("Admin")).OrderBy(x => x.OrderNo))
                .ThenInclude(i => i.Children.OrderBy(x => x.OrderNo).Where(c => _roleMenus.Contains(c.Id) || (c.ApplicationMenuId != null && _roleMenus.Contains((Guid)c.ApplicationMenuId)) || userRoles.Contains("Admin")).OrderBy(x => x.OrderNo))
                //.Where(x=> parents.Contains(x.Id))
                .OrderBy(x => x.OrderNo)
                .ToListAsync();


            if (menus.Count() <= 0) throw new ApiException("رکوردی یافت  نشد !");

            var result = _mapper.Map<List<ApplicationMenuViewModel>>(menus);
            return new Response<IEnumerable<ApplicationMenuViewModel>>(result);
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



        //-------------------------------------------------------------

        public List<ApplicationMenu> GetAccessibleMenusForRole(Guid roleId)
        {
            // پیدا کردن نقش
            var role = _dbContext.Roles.FirstOrDefault(r => r.Id == roleId);
            if (role == null)
            {
                throw new ArgumentException("نقش معتبر نیست.");
            }

            // گرفتن شناسه منوی‌های مرتبط با نقش
            var roleMenuIds = role.RoleMenus.Select(rm => rm.ApplicationMenuId).ToList();

            // ساخت ساختار سلسله مراتبی
            return BuildMenuHierarchy(roleMenuIds);
        }

        private List<ApplicationMenu> BuildMenuHierarchy(List<Guid> roleMenuIds)
        {
            // گرفتن منوی‌های ریشه که به نقش دسترسی دارند
            var rootMenus = _dbContext.ApplicationMenus
                .Where(m => m.ApplicationMenuId == null && roleMenuIds.Contains(m.Id))
                .ToList();

            var hierarchicalMenus = new List<ApplicationMenu>();

            foreach (var rootMenu in rootMenus)
            {
                // ساخت ساختار سلسله مراتبی برای فرزندان
                rootMenu.Children = BuildMenuHierarchy(roleMenuIds, rootMenu.Children.ToList());
                hierarchicalMenus.Add(rootMenu);
            }

            return hierarchicalMenus;
        }

        private List<ApplicationMenu> BuildMenuHierarchy(List<Guid> roleMenuIds, List<ApplicationMenu> menus)
        {
            var accessibleChildren = menus.Where(m => roleMenuIds.Contains(m.Id)).ToList();
            foreach (var childMenu in accessibleChildren)
            {
                childMenu.Children = BuildMenuHierarchy(roleMenuIds, childMenu.Children.ToList());
            }
            return accessibleChildren;
        }
    }
}
