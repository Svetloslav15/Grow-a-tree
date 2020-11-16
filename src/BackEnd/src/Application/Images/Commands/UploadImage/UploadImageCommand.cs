namespace GrowATree.Application.Images.Commands.UploadImage
{
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.Images;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class UploadImageCommand : IRequest<Result<ImageModel>>
    {
        public IFormFile ImageFile { get; set; }
    }
}
