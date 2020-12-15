namespace GrowATree.Application.TreePosts.Commands.Upsert
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

    public class UpsertTreePostCommandHandler : IRequestHandler<UpsertTreePostCommand, Result<bool>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIdentityService identityService;

        public UpsertTreePostCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }

        public async Task<Result<bool>> Handle(UpsertTreePostCommand request, CancellationToken cancellationToken)
        {
            TreePost entity;
            if (!string.IsNullOrEmpty(request.Id))
            {
                entity = await this.context.TreePosts
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity == null)
                {
                    return Result<bool>.Failure(ErrorMessages.TreePostNotFoundErrorMessage);
                }

                entity.Content = request.Content;
            }
            else
            {
                entity = new TreePost
                {
                    Content = request.Content,
                    CreatedOn = DateTime.Now,
                    TreeId = request.TreeId,
                    UserId = await this.identityService.GetCurrentUserId(),
                };

                await this.context.TreePosts.AddAsync(entity);
            }

            await this.context.SaveChangesAsync(CancellationToken.None);

            return Result<bool>.Success(true);
        }
    }
}
