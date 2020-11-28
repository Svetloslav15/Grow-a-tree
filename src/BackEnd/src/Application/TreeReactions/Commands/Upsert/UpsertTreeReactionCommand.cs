namespace GrowATree.Application.TreeReactions.Commands.Upsert
{
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.TreeReactions;
    using MediatR;

    public class UpsertTreeReactionCommand : IRequest<Result<bool>>
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string TreeId { get; set; }

        public string Type { get; set; }
    }
}
