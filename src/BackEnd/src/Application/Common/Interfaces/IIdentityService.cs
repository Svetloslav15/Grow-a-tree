namespace GrowATree.Application.Common.Interfaces
{
    using System.Threading.Tasks;
    using global::Application.Models.Auth.ViewModels;
    using GrowATree.Application.Common.Models;

    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<Result<TokenModel>> LoginAsync(string email, string password);

        //Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        //Task<Result> DeleteUserAsync(string userId);
    }
}
