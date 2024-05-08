using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sepehr.Application.DTOs.Account;
using Sepehr.Application.DTOs.Email;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Enums;
using Sepehr.Domain.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using AutoMapper;
using Sepehr.Domain.ViewModels;
using Sepehr.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace Sepehr.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IdentityContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        public AccountService(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            IMapper mapper,
            IdentityContext dbContext
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            this._emailService = emailService;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new ApiException($"کاربر با این مشخصات یافت نشد !.");
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new ApiException($"خطا در ورود .");
            }
            //if (!user.EmailConfirmed)
            //{
            //    throw new ApiException($"Account Not Confirmed for '{request.UserName}'.");
            //}
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id.ToString();
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = user.Email;
            response.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;
            return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                throw new ApiException($"کاربر با نام کاربری '{request.UserName}' قبلا ثبت شده است .");
            }
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
            };
            //var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            //if (userWithSameEmail == null)
            //{
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                var verificationUri = await SendVerificationEmail(user, origin);
                //TODO: Attach Email Service here and configure it via appsettings
                // await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest()
                // {
                //     From = "mail@codewithmukesh.com",
                //     To = user.Email,
                //     Body = $"Please confirm your account by visiting this URL {verificationUri}",
                //     Subject = "Confirm Registration"
                // });
                return new Response<string>(user.Id.ToString(), message: $"کاربر جدید با موفقیت ایجاد شد .");
            }
            else
            {
                throw new ApiException($"{result.Errors}");
            }
            //}
            //else
            //{
            //    throw new ApiException($"Email {request.Email} is already registered.");
            //}
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("uid", user.Id.ToString()),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/account/confirm-email/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id.ToString());
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            //Email Service Call Here
            return verificationUri;
        }

        public async Task<Response<string>> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return new Response<string>(user.Id.ToString(), message: $"Account Confirmed for {user.Email}. You can now use the /api/Account/authenticate endpoint.");
            }
            else
            {
                throw new ApiException($"An error occured while confirming {user.Email}.");
            }
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = await _userManager.FindByNameAsync(model.UserName);

            // always return ok response to prevent email enumeration
            if (account == null) return;

            var code = await _userManager.GeneratePasswordResetTokenAsync(account);
            var route = "api/account/reset-password/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var emailRequest = new EmailRequest()
            {
                Body = $"توکن جدید شما - {code}",
                To = model.UserName,
                Subject = "بازیابی کلمه عبور",
            };
            await _emailService.SendAsync(emailRequest);
        }

        public async Task<Response<string>> ResetPassword(ResetPasswordRequest model)
        {
            var account = await _userManager.FindByNameAsync(model.UserName);
            if (account == null) throw new ApiException($"کاربر یافت نشد !");
            var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);
            if (result.Succeeded)
            {
                return new Response<string>(model.UserName, message: $"کلمه عبور با موفقیت تغییر کرد .");
            }
            else
            {
                throw new ApiException($"خطا در تغییر کلمه عبور.");
            }
        }

        public async Task<Response<AuthenticationResponse>> RefreshTokenCheckAsync(string? refreshToken)
        {
            //var auth = new AuthResponse();

            ////find the user that match the sent refresh token
            //var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            //if (user == null)
            //{
            //    auth.Message = "Invalid Token";
            //    return auth;
            //}

            //// check if the refreshtoken is active
            //var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            //if (!refreshToken.IsActive)
            //{
            //    auth.Message = "Inactive Token";
            //    return auth;
            //}

            ////revoke the sent Refresh Tokens
            //refreshToken.RevokedOn = DateTime.UtcNow;

            //var newRefreshToken = GenerateRefreshToken();
            //user.RefreshTokens.Add(newRefreshToken);
            //await _userManager.UpdateAsync(user);

            //var jwtSecurityToken = await CreateJwtAsync(user);

            //var roles = await _userManager.GetRolesAsync(user);

            //auth.Email = user.Email;
            //auth.Roles = roles.ToList();
            //auth.ISAuthenticated = true;
            //auth.UserName = user.UserName;
            //auth.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            //auth.TokenExpiresOn = jwtSecurityToken.ValidTo;
            //auth.RefreshToken = newRefreshToken.Token;
            //auth.RefreshTokenExpiration = newRefreshToken.ExpireOn;

            //return auth;

            throw new Exception();
        }

        public async Task<IEnumerable<ApplicationUserViewModel>?> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return _mapper.Map<IEnumerable<ApplicationUserViewModel>>(users);
        }

        public async Task<IEnumerable<ApplicationRoleViewModel>> GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return _mapper.Map<IEnumerable<ApplicationRoleViewModel>>(roles);
        }
    }
}
