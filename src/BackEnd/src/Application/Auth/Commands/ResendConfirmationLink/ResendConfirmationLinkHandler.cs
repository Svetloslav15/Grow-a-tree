namespace GrowATree.Application.Auth.Commands.ResendConfirmationLink
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using global::Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class ResendConfirmationLinkHandler : IRequestHandler<ResendConfirmationLinkCommand, Result<bool>>
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailSender emailSender;

        public ResendConfirmationLinkHandler(UserManager<User> userManager, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        public async Task<Result<bool>> Handle(ResendConfirmationLinkCommand request, CancellationToken cancellationToken)
        {
            User user = await this.userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Result<bool>.Failure(ErrorMessages.EmailInvalidErrorMessage);
            }
            if (user.EmailConfirmed)
            {
                return Result<bool>.Failure(ErrorMessages.EmailAlreadyConfirmedErrorMessage);
            }

            string token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            string confirmationLink = Constants.ConfirmEmailLink + "?token=" + token;
            bool result = await this.emailSender.SendEmail(user, confirmationLink, "Grow A Tree: Confirm email");

            if (!result)
            {
                return Result<bool>.Failure(ErrorMessages.EmailSendingErrorMessage);
            }

            return Result<bool>.Success(true);
        }
    }
}