namespace GrowATree.Application.TreePostReplyReactions.Commands.Upsert
{
    using System.ComponentModel.DataAnnotations;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class UpsertTreePostReplyReactionCommand : IRequest<Result<string>>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.ReactionRequiredErrorMessage)]
        public string Type { get; set; }

        public string TreePostReplyId { get; set; }
    }
}
