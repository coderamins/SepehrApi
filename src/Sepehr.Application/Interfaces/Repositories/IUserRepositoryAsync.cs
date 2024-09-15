using Sepehr.Application.Features.ApplicationUsers.Queries.GetAllApplicationUsers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Entities.UserEntities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IApplicationUserRepositoryAsync : IGenericRepositoryAsync<ApplicationUser>
    {
        Task<RefreshToken> AddRefreshTokenAsync(RefreshToken refreshToken, string userName);
        Task<List<ApplicationUser>> GetAllApplicationUsers(GetAllApplicationUsersParameter filter);
        Task<ApplicationUser?> GetApplicationUserInfo(string userName);
        Task<ApplicationUser?> GetApplicationUserInfo(Guid Id);
        Task<ApplicationUser?> GetApplicationUserInfo(string userName, string password);
        Task<string> GetSavedRefreshToken(string? username);
        Task<ApplicationUser> UpdateUserAsync(ApplicationUser applicationUser);
    }
}