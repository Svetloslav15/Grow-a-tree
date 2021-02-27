namespace GrowATree.Application.TreePosts.Commands.Upsert
{
    using System.ComponentModel.DataAnnotations;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class UpsertTreePostCommand : IRequest<Result<bool>>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.TreePostContentRequiredErrorMessage)]
        [MaxLength(Constants.TreePostContentMaxLength, ErrorMessage = ErrorMessages.TreePostContentMaxLengthErrorMessage)]
        public string Content { get; set; }

        public string TreeId { get; set; }
    }
}
