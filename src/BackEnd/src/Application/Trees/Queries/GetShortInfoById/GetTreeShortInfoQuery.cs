namespace GrowATree.Application.Trees.Queries.GetShortInfoById
{
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.Trees;
    using MediatR;

    public class GetTreeShortInfoQuery : IRequest<Result<TreeShortInfoModel>>
    {
        public string Id { get; set; }
    }
}
