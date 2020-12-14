namespace GrowATree.Application.TreeReactions.Commands.DeleteReaction
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class DeleteTreeReactionCommand : IRequest<Result<bool>>
    {
        public string Id { get; set; }
    }
}
