namespace GrowATree.Application.Trees.Commands.RestoreImage
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    class RestoreTreeImageCommandHandler : IRequestHandler<RestoreTreeImageCommand, Result<string>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIdentityService identityService;

        public RestoreTreeImageCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }

        public async Task<Result<string>> Handle(RestoreTreeImageCommand request, CancellationToken cancellationToken)
        {
            var treeImageModel = await this.context.TreeImages
                .FirstOrDefaultAsync(x => x.Id == request.ImageId);

            if (treeImageModel == null)
            {
                return Result<string>.Failure(ErrorMessages.TreeImageNotFoundErrorMessage);
            }


            if ((await this.identityService.GetCurrentUserId()) != treeImageModel.Tree.OwnerId)
            {
                return Result<string>.Failure(ErrorMessages.NotAllowedErrorMessage);
            }


            treeImageModel.IsDeleted = false;
            treeImageModel.DeletedOn = null;
            await this.context.SaveChangesAsync(cancellationToken);

            return Result<string>.Success(treeImageModel.Id);
        }
    }
}
