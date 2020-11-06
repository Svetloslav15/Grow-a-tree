namespace GrowATree.Application.Trees.Commands.DeleteImage
{
    using System.ComponentModel.DataAnnotations;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class DeleteTreeImageCommand : IRequest<Result<string>>
    {
        [Required(ErrorMessage = ErrorMessages.TreeImageRequiredErrorMessage)]
        public string ImageId { get; set; }
    }
}
