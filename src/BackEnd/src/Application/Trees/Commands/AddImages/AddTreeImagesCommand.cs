namespace GrowATree.Application.Trees.Commands.AddImage
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class AddTreeImagesCommand : IRequest<Result<List<string>>>
    {
        [Required(ErrorMessage = ErrorMessages.TreeNotFoundErrorMessage)]
        public string TreeId { get; set; }

        [Required(ErrorMessage = ErrorMessages.TreeImageRequiredErrorMessage)]
        public List<IFormFile> ImagesFiles { get; set; }
    }
}
