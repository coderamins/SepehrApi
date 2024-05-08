using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.DTOs.Account;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string? origin);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
        Task ForgotPassword(ForgotPasswordRequest model, string? origin);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
        Task<Response<AuthenticationResponse>> RefreshTokenCheckAsync(string? refreshToken);
        Task<IEnumerable<ApplicationUserViewModel>> GetAllUsers();
        Task<IEnumerable<ApplicationRoleViewModel>> GetAllRoles();
    }
}