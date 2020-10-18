namespace GrowATree.Application.Trees.Commands.DeleteImage
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class DeleteTreeImageCommand : IRequest<Result<string>>
    {
        public string ImageId { get; set; }
    }
}
