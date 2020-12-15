namespace GrowATree.Application.TreePostReactions.Commands.Upsert
{
    using System.ComponentModel.DataAnnotations;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class UpsertTreePostReactionCommand : IRequest<Result<string>>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.ReactionRequiredErrorMessage)]
        public string Type { get; set; }

        public string TreePostId { get; set; }
    }
}
