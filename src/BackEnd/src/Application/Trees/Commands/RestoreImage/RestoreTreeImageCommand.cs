namespace GrowATree.Application.Trees.Commands.RestoreImage
{
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using System.ComponentModel.DataAnnotations;

    public class RestoreTreeImageCommand : IRequest<Result<string>>
    {
        [Required(ErrorMessage = ErrorMessages.TreeImageRequiredErrorMessage)]
        public string ImageId { get; set; }
    }
}
