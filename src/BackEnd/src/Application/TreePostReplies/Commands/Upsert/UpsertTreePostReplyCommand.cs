namespace GrowATree.Application.TreePostReplies.Commands.Upsert
{
    using System.ComponentModel.DataAnnotations;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class UpsertTreePostReplyCommand : IRequest<Result<string>>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.TreePostReplyContentRequiredErrorMessage)]
        [MaxLength(Constants.TreePostContentMaxLength, ErrorMessage = ErrorMessages.TreePostReplyMaxLengthRequiredErrorMessage)]
        public string Content { get; set; }

        public string TreePostId { get; set; }
    }
}
