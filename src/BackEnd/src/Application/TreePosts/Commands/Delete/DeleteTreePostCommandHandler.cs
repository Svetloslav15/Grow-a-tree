namespace GrowATree.Application.TreePosts.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class DeleteTreePostCommandHandler : IRequestHandler<DeleteTreePostCommand, Result<bool>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIdentityService identityService;

        public DeleteTreePostCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }

        public async Task<Result<bool>> Handle(DeleteTreePostCommand request, CancellationToken cancellationToken)
        {
            var tree = await this.context.TreePosts
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (tree == null)
            {
                return Result<bool>.Failure(ErrorMessages.TreePostNotFoundErrorMessage);
            }

            if (tree.UserId != await this.identityService.GetCurrentUserId())
            {
                return Result<bool>.Failure(ErrorMessages.NotAllowedErrorMessage);
            }

            tree.IsDeleted = true;
            await this.context.SaveChangesAsync(CancellationToken.None);

            return Result<bool>.Success(true);
        }
    }
}
