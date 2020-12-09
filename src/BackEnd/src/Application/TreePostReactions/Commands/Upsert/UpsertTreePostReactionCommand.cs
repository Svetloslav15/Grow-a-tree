namespace GrowATree.Application.TreePostReactions.Commands.Upsert
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class UpsertTreePostReactionCommand : IRequest<Result<string>>
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string TreePostId { get; set; }
    }
}
