namespace GrowATree.Application.Auth.Commands.ResetPassword
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<bool>>
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identityService;

        public ResetPasswordCommandHandler(UserManager<User> userManager, IIdentityService identityService)
        {
            this.userManager = userManager;
            this.identityService = identityService;
        }

        public async Task<Result<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await this.userManager.FindByEmailAsync(request.Email);

            if ((await this.identityService.GetCurrentUserId()) != user.Id)
            {
                return Result<bool>.Failure(ErrorMessages.NotAllowedErrorMessage);
            }

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
                return Result<bool>.Failure(ErrorMessages.PasswordRequirmentsErrorMessage);
            }

            return Result<bool>.Success(true);
        }
    }
}