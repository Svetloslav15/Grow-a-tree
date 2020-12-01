namespace GrowATree.Application.TreeReactions.Commands.Upsert
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
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UpsertTreeReactionCommandHandler : IRequestHandler<UpsertTreeReactionCommand, Result<bool>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIdentityService identityService;
        private readonly UserManager<User> userManager;

        public UpsertTreeReactionCommandHandler(IApplicationDbContext context, UserManager<User> userManager, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
            this.userManager = userManager;
        }

        public async Task<Result<bool>> Handle(UpsertTreeReactionCommand request, CancellationToken cancellationToken)
        {
            if (!await this.context.Trees.AnyAsync(x => x.Id == request.TreeId))
            {
                return Result<bool>.Failure(ErrorMessages.TreeNotFoundErrorMessage);
            }

            var user = await this.userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return Result<bool>.Failure(ErrorMessages.UserNotFoundErrorMessage);
            }

            if (await this.identityService.GetCurrentUserId() != request.UserId)
            {
                return Result<bool>.Failure(ErrorMessages.NotAllowedErrorMessage);
            }

            TreeReaction entity;
            if (!string.IsNullOrWhiteSpace(request.Id))
            {
                entity = await this.context.TreeReactions
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                entity.Type = (ReactionType)Enum.Parse(typeof(ReactionType), request.Type);
            }
            else
            {
                entity = await this.context.TreeReactions.FirstOrDefaultAsync(x => x.TreeId == request.TreeId && x.UserId == request.UserId);
                if (entity != null)
                {
                    entity.Type = (ReactionType)Enum.Parse(typeof(ReactionType), request.Type);
                }
                else
                {
                    entity = new TreeReaction
                    {
                        TreeId = request.TreeId,
                        UserId = request.UserId,
                        Type = (ReactionType)Enum.Parse(typeof(ReactionType), request.Type),
                        CreatedOn = DateTime.Now,
                    };

                    await this.context.TreeReactions.AddAsync(entity);
                }
            }

            await this.context.SaveChangesAsync(CancellationToken.None);
            return Result<bool>.Success(true);
        }
    }
}
