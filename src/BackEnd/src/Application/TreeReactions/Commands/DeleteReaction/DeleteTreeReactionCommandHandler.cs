namespace GrowATree.Application.TreeReactions.Commands.DeleteReaction
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class DeleteTreeReactionCommandHandler : IRequestHandler<DeleteTreeReactionCommand, Result<bool>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIdentityService identityService;

        public DeleteTreeReactionCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }

        public async Task<Result<bool>> Handle(DeleteTreeReactionCommand request, CancellationToken cancellationToken)
        {
            var reaction = await this.context.TreeReactions
                .Include(x => x.Tree.Owner)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (reaction == null)
            {
                throw new NullReferenceException();
            }

            if (await this.identityService.GetCurrentUserId() != reaction.UserId)
            {
                return Result<bool>.Failure(ErrorMessages.NotAllowedErrorMessage);
            }

            this.context.TreeReactions.Remove(reaction);
            await this.context.SaveChangesAsync(CancellationToken.None);

            return Result<bool>.Success(true);
        }
    }
}
