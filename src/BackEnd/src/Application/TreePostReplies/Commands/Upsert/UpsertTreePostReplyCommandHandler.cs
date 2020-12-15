namespace GrowATree.Application.TreePostReplies.Commands.Upsert
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class UpsertTreePostReplyCommandHandler : IRequestHandler<UpsertTreePostReplyCommand, Result<string>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIdentityService identityService;

        public UpsertTreePostReplyCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }

        public async Task<Result<string>> Handle(UpsertTreePostReplyCommand request, CancellationToken cancellationToken)
        {
            TreePostReply entity;
            if (!string.IsNullOrEmpty(request.Id))
            {
                entity = await this.context.TreePostReplies
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                {
                    return Result<string>.Failure(ErrorMessages.TreePostReplyNotFoundErrorMessage);
                }

                if (await this.identityService.GetCurrentUserId() != entity.UserId)
                {
                    return Result<string>.Failure(ErrorMessages.NotAllowedErrorMessage);
                }

                entity.Content = request.Content;
            }
            else
            {
                if (!await this.context.TreePosts.AnyAsync(x => x.Id == request.TreePostId))
                {
                    return Result<string>.Failure(ErrorMessages.TreePostNotFoundErrorMessage);
                }

                entity = new TreePostReply
                {
                    UserId = await this.identityService.GetCurrentUserId(),
                    CreatedOn = DateTime.Now,
                    TreePostId = request.TreePostId,
                    Content = request.Content,
                };

                await this.context.TreePostReplies.AddAsync(entity);
            }

            await this.context.SaveChangesAsync(CancellationToken.None);

            return Result<string>.Success(entity.Id);
        }
    }
}
