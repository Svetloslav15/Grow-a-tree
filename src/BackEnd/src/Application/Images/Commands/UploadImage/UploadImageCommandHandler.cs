namespace GrowATree.Application.Images.Commands.UploadImage
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using global::Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.Images;
    using MediatR;

    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, Result<ImageModel>>
    {
        private readonly ICloudinaryService cloudinaryService;

        public UploadImageCommandHandler(ICloudinaryService cloudinaryService)
        {
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<Result<ImageModel>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            // Image will be uploaded but it will be not stored in the db
            if (!this.cloudinaryService.IsFileValid(request.ImageFile))
            {
                return Result<ImageModel>.Failure(ErrorMessages.TreeImageInvalidFormatErrorMessage);
            }

            var uploadedImage = await this.cloudinaryService.UploudAsync(request.ImageFile);
            var result = new ImageModel
            {
                Url = uploadedImage,
            };

            return Result<ImageModel>.Success(result);
        }
    }
}
