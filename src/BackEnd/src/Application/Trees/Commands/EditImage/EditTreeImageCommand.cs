namespace GrowATree.Application.Trees.Commands.EditImage
{
    using System.ComponentModel.DataAnnotations;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class EditTreeImageCommand : IRequest<Result<string>>
    {
        [Required(ErrorMessage = ErrorMessages.TreeImageRequiredErrorMessage)]
        public string Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.TreeImageRequiredErrorMessage)]
        public IFormFile NewImageFile { get; set; }
    }
}
