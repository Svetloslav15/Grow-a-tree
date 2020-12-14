namespace GrowATree.Application.TreePostReplies.Commands.Delete
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class DeleteTreePostReplyCommand : IRequest<Result<bool>>
    {
        public string Id { get; set; }
    }
}
