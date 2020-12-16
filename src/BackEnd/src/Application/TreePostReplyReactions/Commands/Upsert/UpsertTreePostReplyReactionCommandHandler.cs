namespace GrowATree.Application.TreePostReplyReactions.Commands.Upsert
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using GrowATree.Domain.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class UpsertTreePostReplyReactionCommandHandler : IRequestHandler<UpsertTreePostReplyReactionCommand, Result<string>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIdentityService identityService;

        public UpsertTreePostReplyReactionCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }

        public async Task<Result<string>> Handle(UpsertTreePostReplyReactionCommand request, CancellationToken cancellationToken)
        {
            TreePostReplyReaction entity;
            if (!string.IsNullOrEmpty(request.Id))
            {
                entity = await this.context.TreePostReplyReactions
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                {
                    return Result<string>.Failure(ErrorMessages.TreePostReplyReactionNotFoundErrorMessage);
                }

                if (entity.UserId != await this.identityService.GetCurrentUserId())
                {
                    return Result<string>.Failure(ErrorMessages.NotAllowedErrorMessage);
                }

                entity.Type = (ReactionType)Enum.Parse(typeof(ReactionType), request.Type);
            }
            else
            {
                if (!await this.context.TreePostReplies.AnyAsync(x => x.Id == request.TreePostReplyId))
                {
                    return Result<string>.Failure(ErrorMessages.TreePostReplyNotFoundErrorMessage);
                }

                var userId = await this.identityService.GetCurrentUserId();
                entity = await this.context.TreePostReplyReactions.FirstOrDefaultAsync(x => x.UserId == userId
                && x.TreePostReplyId == request.TreePostReplyId);
                if (entity != null)
                {
                    entity.Type = (ReactionType)Enum.Parse(typeof(ReactionType), request.Type);
                }
                else
                {
                    entity = new TreePostReplyReaction
                    {
                        TreePostReplyId = request.TreePostReplyId,
                        CreatedOn = DateTime.Now,
                        Type = (ReactionType)Enum.Parse(typeof(ReactionType), request.Type),
                        UserId = await this.identityService.GetCurrentUserId(),
                    };

                    await this.context.TreePostReplyReactions.AddAsync(entity);
                }
            }

            await this.context.SaveChangesAsync(CancellationToken.None);

            return Result<string>.Success(entity.Id);
        }
    }
}
