namespace GrowATree.Application.Trees.Queries.GetClosestTrees
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.Trees;
    using MediatR;

    public class GetClosestTreesShortInfoQuery : PagedQuery, IRequest<TreeListShortInfoModel>
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
