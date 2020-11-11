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
        private readonly IIdentityService identityService;

        public EditTreeImageCommandHandler(IApplicationDbContext context, ICloudinaryService cloudinaryService, IIdentityService identityService)
        {
            this.context = context;
            this.cloudinaryService = cloudinaryService;
            this.identityService = identityService;
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

            if ((await this.identityService.GetCurrentUserId()) != imageModel.Tree.OwnerId)
            {
                return Result<string>.Failure(ErrorMessages.NotAllowedErrorMessage);
            }

            var newImageUrl = await this.cloudinaryService.UploudAsync(request.newImageFile);

            imageModel.Url = newImageUrl;
            await this.context.SaveChangesAsync(cancellationToken);

            return Result<string>.Success(imageModel.Url);
        }
    }
}
