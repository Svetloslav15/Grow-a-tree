namespace GrowATree.Application.Auth.Commands.Register
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Application.Models.Auth.ViewModels;
    using global::Common.Messages;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<TokenModel>>
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identityService;

        public RegisterCommandHandler(UserManager<User> userManager, IIdentityService identityService)
        {
            this.userManager = userManager;
            this.identityService = identityService;
        }

        public async Task<Result<TokenModel>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await this.userManager.FindByEmailAsync(request.Email) != null)
            {
                return Result<TokenModel>.Failure(ErrorMessages.EmailInUseErrorMessage);
            }

            if (await this.userManager.FindByNameAsync(request.Username) != null)
            {
                return Result<TokenModel>.Failure(ErrorMessages.UsernameInUseErrorMessage);
            }

            var user = new User()
            {
                UserName = request.Username,
                Email = request.Email,
                EmailConfirmed = true,
                LockoutEnabled = false,
                City = request.City,
            };

            var identityResult = await this.userManager.CreateAsync(user, request.Password);

            if (!identityResult.Succeeded)
            {
                return Result<TokenModel>.Failure(ErrorMessages.AccountFailureErrorMessage);
            }

            var result = await this.identityService.LoginAsync(user.Email, request.Password);

            return result;
        }
    }
}
