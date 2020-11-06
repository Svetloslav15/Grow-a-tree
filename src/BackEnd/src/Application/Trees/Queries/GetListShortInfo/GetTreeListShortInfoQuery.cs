namespace GrowATree.Application.Trees.Queries.GetListShortInfo
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.Trees;
    using MediatR;

    public class GetTreeListShortInfoQuery : PagedQuery, IRequest<TreeListShortInfoModel>
    {
    }
}
