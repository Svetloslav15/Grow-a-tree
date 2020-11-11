namespace GrowATree.Application.Trees.Commands.DeleteImage
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class DeleteTreeImageCommandHandler : IRequestHandler<DeleteTreeImageCommand, Result<string>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIdentityService identityService;

        public DeleteTreeImageCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }

        public async Task<Result<string>> Handle(DeleteTreeImageCommand request, CancellationToken cancellationToken)
        {
            var treeImageModel = await this.context.TreeImages
                .FirstOrDefaultAsync(x => x.Id == request.ImageId);

            if ((await this.identityService.GetCurrentUserId()) != treeImageModel.Tree.OwnerId)
            {
                return Result<string>.Failure(ErrorMessages.NotAllowedErrorMessage);
            }

            if (treeImageModel == null)
            {
                return Result<string>.Failure(ErrorMessages.TreeImageNotFoundErrorMessage);
            }

            treeImageModel.IsDeleted = true;
            treeImageModel.DeletedOn = DateTime.Now;
            await this.context.SaveChangesAsync(cancellationToken);

            return Result<string>.Success(treeImageModel.Id);
        }
    }
}
