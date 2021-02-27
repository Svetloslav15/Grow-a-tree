namespace GrowATree.Application.TreePostReplyReactions.Queries.GetList
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreePostReplyReactions;
    using MediatR;

    public class GetTreePostReplyReactionListQuery : PagedQuery, IRequest<TreePostReplyReactionListModel>
    {
    }
}
