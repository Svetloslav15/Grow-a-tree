namespace GrowATree.Application.TreePosts.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class DeleteTreePostCommandHandler : IRequestHandler<DeleteTreePostCommand, Result<bool>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIdentityService identityService;
        private readonly UserManager<User> userManager;

        public DeleteTreePostCommandHandler(IApplicationDbContext context, IIdentityService identityService,
            UserManager<User> userManager)
        {
            this.context = context;
            this.identityService = identityService;
            this.userManager = userManager;
        }

        public async Task<Result<bool>> Handle(DeleteTreePostCommand request, CancellationToken cancellationToken)
        {
            var tree = await this.context.TreePosts
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (tree == null)
            {
                return Result<bool>.Failure(ErrorMessages.TreePostNotFoundErrorMessage);
            }

            if (tree.UserId == await this.identityService.GetCurrentUserId()
                || await this.userManager.IsInRoleAsync(await this.userManager.FindByIdAsync(await this.identityService.GetCurrentUserId()),
                Constants.AdminRoleName))
            {
                tree.IsDeleted = true;
                await this.context.SaveChangesAsync(CancellationToken.None);

                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(ErrorMessages.NotAllowedErrorMessage);
        }
    }
}
