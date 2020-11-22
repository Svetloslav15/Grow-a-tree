namespace GrowATree.Application.Auth.Commands.ConfirmEmail
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
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

            if (user == null)
            {
                return Result<bool>.Failure(ErrorMessages.EmailInvalidErrorMessage);
            }

            if (user.EmailConfirmed)
            {
                return Result<bool>.Failure(ErrorMessages.EmailAlreadyConfirmedErrorMessage);
            }
            request.Token = HttpUtility.UrlDecode(request.Token);
            var result = await this.userManager.ConfirmEmailAsync(user, request.Token);
            if (!result.Succeeded)
            {
                if (result.Errors.ToList()[0].Code == "InvalidToken")
                {
                    return Result<bool>.Failure(ErrorMessages.ConfirmTokenInvalidErrorMessage);
                }
            }
            return result.Succeeded ? Result<bool>.Success(true) : Result<bool>.Failure(ErrorMessages.GeneralSomethingWentWrong);
        }
    }
}