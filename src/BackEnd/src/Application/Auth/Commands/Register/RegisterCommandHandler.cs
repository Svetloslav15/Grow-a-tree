﻿namespace GrowATree.Application.Auth.Commands.Register
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using global::Common.Constants;
    using global::Common.Interfaces;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Serilog;

    /// <summary>
    /// Handler that implement register logic.
    /// </summary>
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<bool>>
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailSender emailSender;
        private readonly IApplicationDbContext context;

        public RegisterCommandHandler(UserManager<User> userManager, IEmailSender emailSender, IApplicationDbContext context)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.context = context;
        }

        public async Task<Result<bool>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            Log.Logger.Error("Handler Beggining: " + request.Email);
            if (await this.userManager.FindByEmailAsync(request.Email) != null)
            {
                return Result<bool>.Failure(ErrorMessages.EmailInUseErrorMessage);
            }

            if (await this.userManager.FindByNameAsync(request.Username) != null)
            {
                return Result<bool>.Failure(ErrorMessages.UsernameInUseErrorMessage);
            }

            var referrer = await this.userManager.FindByIdAsync(request.ReferrerId);
            var user = new User
            {
                UserName = request.Username,
                Email = request.Email,
                EmailConfirmed = false,
                LockoutEnabled = false,
                City = request.City,
                ReferrerId = referrer != null ? referrer.Id : null,
                ProfilePictureUrl = "https://res.cloudinary.com/dzivpr6fj/image/upload/v1602432685/GrowATree/avatar_dpskn1.png"
            };
            var identityResult = await this.userManager.CreateAsync(user, request.Password);
            
            if (!identityResult.Succeeded)
            {
                return Result<bool>.Failure(ErrorMessages.PasswordRequirmentsErrorMessage);
            }

            string token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            token = HttpUtility.UrlEncode(token);
            Log.Logger.Error("Handler After creatting user: " + user.Email);

            string confirmationLink = Constants.ConfirmEmailLink + "?token=" + token;
            bool result = await this.emailSender.SendEmail(user, confirmationLink, "Grow A Tree: Confirm email");

            if (!result)
            {
                return Result<bool>.Failure(ErrorMessages.EmailSendingErrorMessage);
            }
            user.LockoutEnabled = false;
            await this.context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}
