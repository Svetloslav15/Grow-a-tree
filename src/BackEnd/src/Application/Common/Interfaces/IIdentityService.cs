namespace GrowATree.Application.Common.Interfaces
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using global::Application.Models.Auth;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;

    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<Result<TokenModel>> LoginAsync(string email, string password);

        Task<TokenModel> GenerateTokenModel(User user);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}