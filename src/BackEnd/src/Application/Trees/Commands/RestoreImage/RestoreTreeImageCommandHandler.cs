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

        public RestoreTreeImageCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<string>> Handle(RestoreTreeImageCommand request, CancellationToken cancellationToken)
        {
            var treeImageModel = await this.context.TreeImages
                .FirstOrDefaultAsync(x => x.Id == request.ImageId);

            if (treeImageModel == null)
            {
                return Result<string>.Failure(ErrorMessages.TreeImageNotFoundErrorMessage);
            }

            treeImageModel.IsDeleted = false;
            treeImageModel.DeletedOn = null;
            await this.context.SaveChangesAsync(cancellationToken);

            return Result<string>.Success(treeImageModel.Id);
        }
    }
}
