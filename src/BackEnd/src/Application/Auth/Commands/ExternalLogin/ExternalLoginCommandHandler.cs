﻿namespace GrowATree.Application.Auth.Commands.FacebookLogin
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
        private readonly ICloudinaryService cloudinaryService;

        public ExternalLoginCommandHandler(
            UserManager<User> userManager,
            IIdentityService identityService,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            ICloudinaryService cloudinaryService)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.signInManager = signInManager;
            this.configuration = configuration;
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
                    var newUser = new User
                    {
                        Email = request.Email,
                        UserName = request.FirstName + " " + request.LastName,
                        ProfilePictureUrl = request.ProviderName == "Google"
                        ? request.ProfilePictureUrl
                        : this.cloudinaryService.IsFileValid(request.ProfilePictureFile)
                            ? await this.cloudinaryService.UploudAsync(request.ProfilePictureFile)
                            : Constants.DefaultProfilePictureUrl,
                        EmailConfirmed = true,
                    };
                    var info = new ExternalLoginInfo(ClaimsPrincipal.Current, request.ProviderName, request.ProviderKey, request.ProviderName);
                    var identityResult = await this.userManager.CreateAsync(newUser);

                    if (identityResult.Succeeded)
                    {
                        var result = await this.userManager.AddLoginAsync(newUser, info);

                        return result.Succeeded ? await this.identityService.ExternalLoginAsync(request.ProviderName, request.ProviderKey) : Result<TokenModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
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
            else
            {
                return Result<TokenModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }
    }
}