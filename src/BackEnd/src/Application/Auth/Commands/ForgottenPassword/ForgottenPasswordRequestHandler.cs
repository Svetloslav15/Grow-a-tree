namespace GrowATree.Application.Auth.Commands.ForgottenPassword
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using global::Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class ForgottenPasswordRequestHandler : IRequestHandler<ForgottenPasswordCommand, Result<bool>>
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailSender emailSender;

        public ForgottenPasswordRequestHandler(UserManager<User> userManager, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        public async Task<Result<bool>> Handle(ForgottenPasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await this.userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Result<bool>.Failure(ErrorMessages.EmailInvalidErrorMessage);
            }

            string token = await this.userManager.GeneratePasswordResetTokenAsync(user);
            string forgottenPasswordLink = Constants.ResetPasswordLink + token;
            bool result = await this.emailSender.SendEmail(user, forgottenPasswordLink, "Grow A Tree: Forgotten Password");

            if (!result)
            {
                return Result<bool>.Failure(ErrorMessages.EmailSendingErrorMessage);
            }

            return Result<bool>.Success(true);
        }
    }
}