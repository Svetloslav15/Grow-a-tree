namespace GrowATree.Infrastructure.Identity
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Constants;
    using global::Application.Models.Auth;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        public IdentityService(UserManager<User> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
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
                    expires: DateTime.UtcNow.AddHours(Constants.JwtExpirationTimeInHours),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                var result = new TokenModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expires = token.ValidTo,
                    Id = user.Id,
                };

                return Result<TokenModel>.Success(result);
            }
            else
            {
                return Result<TokenModel>.Failure(ErrorMessages.LoginFailureErrorMessage);
            }
        }
    }
}