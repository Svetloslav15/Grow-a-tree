namespace GrowATree.Application.TreeReactions.Commands.Upsert
{
    using System.ComponentModel.DataAnnotations;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class UpsertTreeReactionCommand : IRequest<Result<bool>>
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string TreeId { get; set; }

        [Required(ErrorMessage = ErrorMessages.ReactionRequiredErrorMessage)]
        public string Type { get; set; }
    }
}
