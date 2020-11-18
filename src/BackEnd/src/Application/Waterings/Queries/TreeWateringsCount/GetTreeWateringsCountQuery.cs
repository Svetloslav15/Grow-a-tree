namespace GrowATree.Application.Waterings.Queries.TreeWateringsCount
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class GetTreeWateringsCountQuery : IRequest<Result<int>>
    {
        public string TreeId { get; set; }
    }
}
