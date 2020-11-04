namespace GrowATree.Application.Trees.Queries.GetUserTreesShortInfo
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.Trees;
    using MediatR;

    public class GetUserTreesShortInfoQuery : PagedQuery, IRequest<TreeListShortInfoModel>
    {
        public string Id { get; set; }
    }
}
