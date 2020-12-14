namespace GrowATree.Application.TreePostReplies.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class DeleteTreePostReplyCommandHandler : IRequestHandler<DeleteTreePostReplyCommand, Result<bool>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIdentityService identityService;

        public DeleteTreePostReplyCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }

        public async Task<Result<bool>> Handle(DeleteTreePostReplyCommand request, CancellationToken cancellationToken)
        {
            var entity = await this.context.TreePostReplies
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
            {
                return Result<bool>.Failure(ErrorMessages.TreePostReplyNotFoundErrorMessage);
            }

            if (entity.UserId != await this.identityService.GetCurrentUserId())
            {
                return Result<bool>.Failure(ErrorMessages.NotAllowedErrorMessage);
            }

            entity.IsDeleted = true;
            await this.context.SaveChangesAsync(CancellationToken.None);

            return Result<bool>.Success(true);
        }
    }
}
