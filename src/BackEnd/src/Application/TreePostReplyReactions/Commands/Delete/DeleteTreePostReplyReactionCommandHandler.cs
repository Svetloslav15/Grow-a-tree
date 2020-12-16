namespace GrowATree.Application.TreePostReplyReactions.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class DeleteTreePostReplyReactionCommandHandler : IRequestHandler<DeleteTreePostReplyReactionCommand, Result<bool>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIdentityService identityService;

        public DeleteTreePostReplyReactionCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }

        public async Task<Result<bool>> Handle(DeleteTreePostReplyReactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await this.context.TreePostReplyReactions
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
            {
                return Result<bool>.Failure(ErrorMessages.TreePostReplyReactionNotFoundErrorMessage);
            }

            if (entity.UserId != await this.identityService.GetCurrentUserId())
            {
                return Result<bool>.Failure(ErrorMessages.NotAllowedErrorMessage);
            }

            this.context.TreePostReplyReactions.Remove(entity);
            await this.context.SaveChangesAsync(CancellationToken.None);

            return Result<bool>.Success(true);
        }
    }
}
