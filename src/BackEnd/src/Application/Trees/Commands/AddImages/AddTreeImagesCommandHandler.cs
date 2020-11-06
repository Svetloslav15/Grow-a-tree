namespace GrowATree.Application.Trees.Commands.AddImage
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using global::Common.Interfaces;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class AddTreeImagesCommandHandler : IRequestHandler<AddTreeImagesCommand, Result<List<string>>>
    {
        private readonly IApplicationDbContext context;
        private readonly ICloudinaryService cloudinaryService;

        public AddTreeImagesCommandHandler(IApplicationDbContext context, ICloudinaryService cloudinaryService)
        {
            this.context = context;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<Result<List<string>>> Handle(AddTreeImagesCommand request, CancellationToken cancellationToken)
        {
            var treeModel = await this.context.Trees
                .FirstOrDefaultAsync(x => x.Id == request.TreeId);

            if (treeModel == null)
            {
                return Result<List<string>>.Failure(ErrorMessages.TreeNotFoundErrorMessage);
            }

            if (request.ImagesFiles == null && request.ImagesFiles.Count == 0)
            {
                return Result<List<string>>.Failure(ErrorMessages.TreeImageRequiredErrorMessage);
            }

            var result = new List<string>();
            foreach (var image in request.ImagesFiles)
            {
                if (!this.cloudinaryService.IsFileValid(image))
                {
                    return Result<List<string>>.Failure(ErrorMessages.TreeImageInvalidFormatErrorMessage);
                }

                var imageUrl = await this.cloudinaryService.UploudAsync(image);
                var newTreeImageModel = new TreeImage
                {
                    TreeId = treeModel.Id,
                    Tree = treeModel,
                    Url = imageUrl,
                };

                await this.context.TreeImages.AddAsync(newTreeImageModel);
                result.Add(imageUrl);
            }

            await this.context.SaveChangesAsync(cancellationToken);
            return Result<List<string>>.Success(result);
        }
    }
}
