namespace GrowATree.Application.Auth.Commands.FacebookLogin
{
    using System;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Application.Models.Auth;
    using global::Common.Constants;
    using global::Common.Interfaces;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;

    public class ExternalLoginCommandHandler : IRequestHandler<ExternalLoginCommand, Result<TokenModel>>
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identityService;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;
        private readonly IImageService imageService;
        private readonly ICloudinaryService cloudinaryService;

        public ExternalLoginCommandHandler(
            UserManager<User> userManager,
            IIdentityService identityService,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            ICloudinaryService cloudinaryService,
            IImageService imageService)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.imageService = imageService;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<Result<TokenModel>> Handle(ExternalLoginCommand request, CancellationToken cancellationToken)
        {
            if ((request.ProviderKey == this.configuration["Logins:Google"] && request.ProviderName == "Google") ||
                (request.ProviderKey == this.configuration["Logins:Facebook"] && request.ProviderName == "Facebook"))
            {
                var user = await this.userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    if (await this.userManager.FindByEmailAsync(request.Email) != null)
                    {
                        return Result<TokenModel>.Failure(ErrorMessages.EmailInUseErrorMessage);
                    }

                    var generatedUsername = request.FirstName + request.LastName;
                    while (await this.userManager.FindByNameAsync(generatedUsername) != null)
                    {
                        var random = new Random();
                        generatedUsername = request.FirstName + request.LastName + random.Next(1, 1000);
                    }

                    var imageFile = imageService.ReadImageFromUrl(request.ProfilePictureUrl);
                    var imageUrl = await cloudinaryService.UploudAsync(imageFile);

                    var newUser = new User
                    {
                        Email = request.Email,
                        UserName = generatedUsername,
                        ProfilePictureUrl = imageUrl,
                        EmailConfirmed = true,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                    };
                    var info = new ExternalLoginInfo(ClaimsPrincipal.Current, request.ProviderName, request.UserId, request.ProviderName);
                    var identityResult = await this.userManager.CreateAsync(newUser);

                    if (identityResult.Succeeded)
                    {
                        var result = await this.userManager.AddLoginAsync(newUser, info);

                        return result.Succeeded ? await this.identityService.ExternalLoginAsync(request.ProviderName, request.UserId) : Result<TokenModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
                    }
                    else
                    {
                        return Result<TokenModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
                    }
                }
                else
                {
                    var tokenModel = await this.identityService.ExternalLoginAsync(request.ProviderName, request.UserId);

                    return tokenModel;
                }
            }
            else
            {
                return Result<TokenModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }
    }
}