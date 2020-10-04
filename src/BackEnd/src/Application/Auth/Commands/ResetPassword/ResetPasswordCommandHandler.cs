namespace GrowATree.Application.Auth.Commands.ResetPassword
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<bool>>
    {
        private readonly UserManager<User> userManager;

        public ResetPasswordCommandHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Result<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await this.userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Result<bool>.Failure(ErrorMessages.EmailInvalidErrorMessage);
            }

            if (!user.EmailConfirmed)
            {
                return Result<bool>.Failure(ErrorMessages.EmailNotConfirmedErrorMessage);
            }

            IdentityResult resetPassResult = await this.userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!resetPassResult.Succeeded)
            {
                return Result<bool>.Failure(ErrorMessages.PasswordResetErrorMessage);
            }

            return Result<bool>.Success(true);
        }
    }
}