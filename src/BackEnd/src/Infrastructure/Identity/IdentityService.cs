namespace GrowATree.Infrastructure.Identity
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Constants;
    using global::Application.Models.Auth;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContext;

        public IdentityService(UserManager<User> userManager, IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.httpContext = httpContext;
        }

        public async Task<string> GetCurrentUserId()
        {
            var userName = this.httpContext.HttpContext.User.Identity.Name;
            var user = await this.userManager.Users.FirstAsync(x => x.UserName == userName);
            return user.Id;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await this.userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<Result<TokenModel>> LoginAsync(string email, string password)
        {
            var user = await this.userManager.FindByEmailAsync(email);
            if (user != null && await this.userManager.CheckPasswordAsync(user, password))
            {
                if (!user.EmailConfirmed)
                {
                    return Result<TokenModel>.Failure(ErrorMessages.EmailNotConfirmedErrorMessage);
                }

                var tokenModel = await this.GenerateTokenModel(user);

                user.RefreshToken = tokenModel.RefreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(Constants.JwtExpirationTimeInDays);

                await this.userManager.UpdateAsync(user);

                return Result<TokenModel>.Success(tokenModel);
            }
            else
            {
                return Result<TokenModel>.Failure(ErrorMessages.LoginFailureErrorMessage);
            }
        }

        public async Task<Result<TokenModel>> ExternalLoginAsync(string providerName, string providerKey)
        {
            var user = await this.userManager.FindByLoginAsync(providerName, providerKey);

            if (user != null)
            {
                var tokenModel = await this.GenerateTokenModel(user);

                user.RefreshToken = tokenModel.RefreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(Constants.JwtExpirationTimeInMinutes);

                await this.userManager.UpdateAsync(user);

                return Result<TokenModel>.Success(tokenModel);
            }
            else
            {
                return Result<TokenModel>.Failure(ErrorMessages.LoginFailureErrorMessage);
            }
        }

        public async Task<TokenModel> GenerateTokenModel(User user)
        {
            var userRoles = await this.userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(Constants.JwtExpirationTimeInMinutes),
                claims: authClaims,
                issuer: this.configuration["JWT:ValidIssuer"],
                audience: this.configuration["JWT:ValidAudience"],
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            bool isStore = false;
            if (userRoles.Contains(Constants.StoreRoleName))
            {
                isStore = true;
            }

            var result = new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = this.GenerateRefreshToken(),
                Expires = token.ValidTo,
                Id = user.Id,
                IsStore = isStore,
            };

            return result;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JWT:Secret"])),
                ValidateLifetime = false,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return principal;
        }
    }
}