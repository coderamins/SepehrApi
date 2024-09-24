using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.ApplicationUsers.Queries.GetAllApplicationUsers;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class ApplicationUserRepositoryAsync : GenericRepositoryAsync<ApplicationUser>, IApplicationUserRepositoryAsync
    {
        private readonly DbSet<ApplicationUser> _applicationUsers;
        private readonly DbSet<RefreshToken> _userRefreshToken;
        private readonly ApplicationDbContext _dbContext;

        public ApplicationUserRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _applicationUsers = dbContext.Set<ApplicationUser>();
            _userRefreshToken = dbContext.Set<RefreshToken>();
            _dbContext = dbContext;
        }

        public async Task<RefreshToken> AddRefreshTokenAsync(RefreshToken refreshToken, string userName)
        {
            var userPrevTokens = await _userRefreshToken.Where(t => t.Revoked == null).ToListAsync();
            if (userPrevTokens.Count() > 0)
                userPrevTokens.ForEach(t => t.Revoked = DateTime.Now);

            var reftoken = await _userRefreshToken.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();

            return reftoken.Entity;
        }

        public async Task CreateVerificationCode(VerificationCode verificationCode)
        {
            await _dbContext.VerificationCodes.AddAsync(verificationCode);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeactivateVerifyCode(string verificationCode)
        {
            var vcode =await _dbContext.VerificationCodes.FirstOrDefaultAsync(c => c.Code == verificationCode);
            if (vcode == null)
                throw new ApiException("کد تایید یافت نشد !");
            
            vcode.IsActive = false;
            vcode.IsUsed = true;

            _dbContext.Update(vcode);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ApplicationUser>> GetAllApplicationUsers(GetAllApplicationUsersParameter filter)
        {
            return await _applicationUsers
                .Where(u => filter.UserRoles==null || filter.UserRoles.Count()<=0 || u.Roles.Any(r=> filter.UserRoles.Contains(r.Role.Id.ToString())))
                .OrderByDescending(p => p.Created)
                .ToListAsync();
        }

        public async Task<ApplicationUser?> GetApplicationUserInfo(Guid Id)
        {
            var user = await _applicationUsers
                .Include(u => u.Roles)
                .ThenInclude(u=>u.Role)
                .ThenInclude(u=>u.RolePermissions)
                .ThenInclude(u=>u.Permission)
                .FirstOrDefaultAsync(p => p.Id == Id);

            return user;
        }

        public async Task<ApplicationUser?> GetApplicationUserInfo(string userName)
        {
            var user = await _applicationUsers
                .Include(u => u.Roles).ThenInclude(r=>r.Role)
                .FirstOrDefaultAsync(p => p.UserName == userName);

            return user;
        }

        public async Task<ApplicationUser?> GetApplicationUserInfo(string userName, string password)
        {
            var user = await _applicationUsers
                .FirstOrDefaultAsync(p => p.UserName == userName);

            return user;
        }

        public async Task<string> GetSavedRefreshToken(string? username)
        {
            var user = await _applicationUsers.FirstOrDefaultAsync(u => u.UserName == username);
            if (user.RefreshTokens == null)
                throw new ApiException("احراز هویت امکان پذیر نیست !");

            var refreshToken = user.RefreshTokens.FirstOrDefault(r => r.IsActive);
            if (refreshToken == null)
                throw new ApiException("احراز هویت امکان پذیر نیست !");

            return refreshToken.Token;
        }

        public async Task<bool> HasAnyActiveVerifyCode(string userName)
        {
            return await _dbContext.VerificationCodes
                .AnyAsync(x => x.UserName.Equals(userName) && x.IsActive && x.CreatedAt.AddMinutes(1) > DateTime.Now);
        }

        public async Task<bool> IsValidVerifyCode(string userName,string code)
        {
            return await _dbContext.VerificationCodes.AnyAsync(x => x.UserName.Equals(userName) && x.Code.Equals(code) && x.IsActive && x.CreatedAt.AddMinutes(1) > DateTime.UtcNow);
        }

        public async Task<ApplicationUser> UpdateUserAsync(ApplicationUser applicationUser)
        {
            _dbContext.UserRoles.RemoveRange();

            var result = _applicationUsers.Update(applicationUser);
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }
    }
}