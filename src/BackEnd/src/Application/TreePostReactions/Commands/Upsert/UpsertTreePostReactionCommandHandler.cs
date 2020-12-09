namespace GrowATree.Application.TreePostReactions.Commands.Upsert
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using GrowATree.Domain.Enums;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UpsertTreePostReactionCommandHandler : IRequestHandler<UpsertTreePostReactionCommand, Result<string>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIdentityService identityService;
        private readonly UserManager<User> userManager;

        public UpsertTreePostReactionCommandHandler(IApplicationDbContext context, IIdentityService identityService, UserManager<User> userManager)
        {
            this.context = context;
            this.identityService = identityService;
            this.userManager = userManager;
        }

        public async Task<Result<string>> Handle(UpsertTreePostReactionCommand request, CancellationToken cancellationToken)
        {
            TreePostReaction entity;
            if (!string.IsNullOrEmpty(request.Id))
            {
                entity = await this.context.TreePostReactions
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                {
                    return Result<string>.Failure(ErrorMessages.TreePostReactionNotFoundErrorMessage);
                }

                if (entity.UserId != await this.identityService.GetCurrentUserId())
                {
                    return Result<string>.Failure(ErrorMessages.NotAllowedErrorMessage);
                }

                entity.Type = (ReactionType)Enum.Parse(typeof(ReactionType), request.Type);
            }
            else
            {
                if (!await this.context.TreePosts.AnyAsync(x => x.Id == request.TreePostId))
                {
                    return Result<string>.Failure(ErrorMessages.TreePostReactionNotFoundErrorMessage);
                }

                var userId = await this.identityService.GetCurrentUserId();
                var user = await this.context.Users
                    .Include(x => x.TreePostReactions)
                    .FirstOrDefaultAsync(x => x.Id == userId);

                if (user.TreePostReactions.Any(x => x.PostId == request.TreePostId))
                {
                    return Result<string>.Failure(ErrorMessages.TreePostAlreadyReactedErrorMessage);
                }

                entity = new TreePostReaction
                {
                    CreatedOn = DateTime.Now,
                    PostId = request.TreePostId,
                    UserId = await this.identityService.GetCurrentUserId(),
                    Type = (ReactionType)Enum.Parse(typeof(ReactionType), request.Type),
                };

                await this.context.TreePostReactions.AddAsync(entity);
            }

            await this.context.SaveChangesAsync(CancellationToken.None);

            return Result<string>.Success(entity.Id);
        }
    }
}
