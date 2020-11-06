namespace GrowATree.Application.Trees.Commands.EditImage
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using global::Common.Interfaces;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class EditTreeImageCommandHandler : IRequestHandler<EditTreeImageCommand, Result<string>>
    {
        private readonly IApplicationDbContext context;
        private readonly ICloudinaryService cloudinaryService;

        public EditTreeImageCommandHandler(IApplicationDbContext context, ICloudinaryService cloudinaryService)
        {
            this.context = context;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<Result<string>> Handle(EditTreeImageCommand request, CancellationToken cancellationToken)
        {
            if (!this.cloudinaryService.IsFileValid(request.newImageFile))
            {
                return Result<string>.Failure(ErrorMessages.TreeImageInvalidFormatErrorMessage);
            }

            var imageModel = await this.context.TreeImages
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (imageModel == null)
            {
                return Result<string>.Failure(ErrorMessages.TreeImageNotFoundErrorMessage);
            }

            var newImageUrl = await this.cloudinaryService.UploudAsync(request.newImageFile);

            imageModel.Url = newImageUrl;
            await this.context.SaveChangesAsync(cancellationToken);

            return Result<string>.Success(imageModel.Url);
        }
    }
}
