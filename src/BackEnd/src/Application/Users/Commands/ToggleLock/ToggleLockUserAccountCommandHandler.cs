namespace GrowATree.Application.Users.Commands.ToggleLock
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class ToggleLockUserAccountCommandHandler : IRequestHandler<ToggleLockUserAccountCommand, Result<bool>>
    {
        private readonly IApplicationDbContext context;

        public ToggleLockUserAccountCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<bool>> Handle(ToggleLockUserAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await this.context
                .Users
                .FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (user == null)
            {
                return Result<bool>.Failure(ErrorMessages.UserNotFoundErrorMessage);
            }

            user.LockoutEnabled = !user.LockoutEnabled;
            await this.context.SaveChangesAsync(CancellationToken.None);

            return Result<bool>.Success(true);
        }
    }
}
