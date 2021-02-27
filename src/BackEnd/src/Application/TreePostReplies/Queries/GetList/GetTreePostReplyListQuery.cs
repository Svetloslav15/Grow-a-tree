namespace GrowATree.Application.TreePostReplies.Queries.GetList
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreePostReplies;
    using MediatR;

    public class GetTreePostReplyListQuery : PagedQuery, IRequest<TreePostReplyListModel>
    {
    }
}
