namespace GrowATree.Application.TreePosts.Commands.Delete
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class DeleteTreePostCommand : IRequest<Result<bool>>
    {
        public string Id { get; set; }
    }
}
