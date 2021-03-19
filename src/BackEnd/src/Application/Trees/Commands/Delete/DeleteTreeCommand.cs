namespace GrowATree.Application.Trees.Commands.Delete
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class DeleteTreeCommand : IRequest<Result<string>>
    {
        public string Id { get; set; }
    }
}