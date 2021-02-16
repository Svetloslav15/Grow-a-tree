namespace GrowATree.Application.Trees.Queries.GetRecentTrees
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.Trees;
    using MediatR;

    public class GetRecentTreesQuery : PagedQuery, IRequest<TreeListModel>
    {
    }
}
