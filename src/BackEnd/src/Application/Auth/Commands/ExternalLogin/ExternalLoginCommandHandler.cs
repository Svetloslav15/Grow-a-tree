namespace GrowATree.Application.Auth.Commands.FacebookLogin
{
    using System;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Application.Models.Auth;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class ExternalLoginCommandHandler : IRequestHandler<ExternalLoginCommand, Result<TokenModel>>
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identityService;
        private readonly SignInManager<User> signInManager;

        public ExternalLoginCommandHandler(UserManager<User> userManager, IIdentityService identityService, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.signInManager = signInManager;
        }

        public async Task<Result<TokenModel>> Handle(ExternalLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await this.userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                var newUser = new User
                {
                    Email = request.Email,
                    UserName = request.FirstName + " " + request.LastName,
                    ProfilePictureUrl = request.ProfilePictureUrl,
                    EmailConfirmed = true,
                };
                ExternalLoginInfo info = new ExternalLoginInfo(ClaimsPrincipal.Current, request.ProviderName, request.ProviderKey, request.ProviderName);
                var identityResult = await this.userManager.CreateAsync(newUser);

                if (identityResult.Succeeded)
                {
                    var result = await this.userManager.AddLoginAsync(newUser, info);
                    if (result.Succeeded)
                    {
                        var tokenModel = await this.identityService.ExternalLoginAsync(request.ProviderName, request.ProviderKey);

                        return tokenModel;
                    }
                    else
                    {
                        return Result<TokenModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
                    }
                }
                else
                {
                    return Result<TokenModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
                }
            }
            else
            {
                var tokenModel = await this.identityService.ExternalLoginAsync(request.ProviderName, request.ProviderKey);

                return tokenModel;
            }
        }
    }
}
