namespace GrowATree.Application.Stores.Queries.GetById
{
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.Trees;
    using MediatR;

    public class GetTreeByIdQuery : IRequest<Result<TreeModel>>
    {
        public string Id { get; set; }
    }
}
