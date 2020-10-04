namespace GrowATree.Application.Auth.Commands.ChangeEmail
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using global::Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    
    public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, Result<bool>>
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailSender emailSender;

        public ChangeEmailCommandHandler(UserManager<User> userManager, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        public async Task<Result<bool>> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            request.NewEmail = request.NewEmail.ToLower();
            request.OldEmail = request.OldEmail.ToLower();

            User user = await this.userManager.FindByEmailAsync(request.OldEmail);

            if (user == null)
            {
                return Result<bool>.Failure(ErrorMessages.EmailInvalidErrorMessage);
            }

            if (!user.EmailConfirmed)
            {
                return Result<bool>.Failure(ErrorMessages.EmailNotConfirmedErrorMessage);
            }

            if (request.NewEmail == request.OldEmail)
            {
                return Result<bool>.Failure(ErrorMessages.ChangeEmailDifferentEmailsErrorMessage);
            }

            if (await this.userManager.FindByEmailAsync(request.NewEmail) != null)
            {
                return Result<bool>.Failure(ErrorMessages.EmailInUseErrorMessage);
            }

            string token = await this.userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);
            IdentityResult result = await this.userManager.ChangeEmailAsync(user, request.NewEmail, token);

            if (!result.Succeeded)
            {
                return Result<bool>.Failure(ErrorMessages.ChangeEmailErrorMessage);
            }

            user.EmailConfirmed = false;
            await this.userManager.UpdateAsync(user);

            string confirmToken = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            string confirmationLink = Constants.ConfirmEmailLink + confirmToken;
            bool resultEmailSend = await this.emailSender.SendEmail(user, confirmationLink, "Grow A Tree: Confirm email");

            if (!resultEmailSend)
            {
                return Result<bool>.Failure(ErrorMessages.EmailSendingErrorMessage);
            }

            return Result<bool>.Success(true);
        }
    }
}