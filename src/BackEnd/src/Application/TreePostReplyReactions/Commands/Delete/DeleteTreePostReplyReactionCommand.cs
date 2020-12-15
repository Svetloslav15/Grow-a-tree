namespace GrowATree.Application.TreePostReplyReactions.Commands.Delete
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class DeleteTreePostReplyReactionCommand : IRequest<Result<bool>>
    {
        public string Id { get; set; }
    }
}
