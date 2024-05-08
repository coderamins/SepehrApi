using Sepehr.Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Interfaces
{
    public interface IJwtProvider
    {
        Task<string> Generate(ApplicationUser userInfo);
        RefreshToken GenerateRefreshToken();
        Task<string> GenerateRefreshToken(ApplicationUser username);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
