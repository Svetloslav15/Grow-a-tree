namespace GrowATree.Application.Auth.Commands.Register
{
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

    /// <summary>
    /// Hadler that implement register logic.
    /// </summary>
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<bool>>
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identityService;
        private readonly IEmailSender emailSender;

        public RegisterCommandHandler(UserManager<User> userManager, IIdentityService identityService, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.emailSender = emailSender;
        }

        public async Task<Result<bool>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await this.userManager.FindByEmailAsync(request.Email) != null)
            {
                return Result<bool>.Failure(ErrorMessages.EmailInUseErrorMessage);
            }

            if (await this.userManager.FindByNameAsync(request.Username) != null)
            {
                return Result<bool>.Failure(ErrorMessages.UsernameInUseErrorMessage);
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
                return Result<bool>.Failure(ErrorMessages.AccountFailureErrorMessage);
            }

            string token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            string confirmationLink = $"https://localhost:44312/api/Auth/{token}";
            this.emailSender.SendEmail(user, confirmationLink, "Grow A Tree: Confirm email");

            return Result<bool>.Success(true);
        }
    }
}
