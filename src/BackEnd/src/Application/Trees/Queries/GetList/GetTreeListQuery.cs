namespace GrowATree.Application.Trees.Queries.GetList
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.Trees;
    using MediatR;

    public class GetTreeListQuery : PagedQuery, IRequest<TreeListModel>
    {
    }
}
