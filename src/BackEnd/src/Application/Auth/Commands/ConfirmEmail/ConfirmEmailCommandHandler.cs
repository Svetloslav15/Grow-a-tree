namespace GrowATree.Application.Auth.Commands.ConfirmEmail
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result<bool>>
    {
        private readonly UserManager<User> userManager;

        public ConfirmEmailCommandHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Result<bool>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            User user = await this.userManager.FindByEmailAsync(request.Email);
            if (user.EmailConfirmed)
            {
                return Result<bool>.Failure(ErrorMessages.EmailAlreadyConfirmedErrorMessage);
            }

            if (user == null)
            {
                return Result<bool>.Failure(ErrorMessages.EmailInvalidErrorMessage);
            }

            var result = await this.userManager.ConfirmEmailAsync(user, request.Token);

            return result.Succeeded ? Result<bool>.Success(true) : Result<bool>.Failure(ErrorMessages.GeneralSomethingWentWrong);
        }
    }
}