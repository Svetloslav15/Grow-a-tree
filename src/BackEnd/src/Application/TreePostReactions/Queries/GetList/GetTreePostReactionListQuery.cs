namespace GrowATree.Application.TreePostReactions.Queries.GetList
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreePostReactions;
    using MediatR;

    public class GetTreePostReactionListQuery : PagedQuery, IRequest<TreePostReactionListModel>
    {
        public string PostId { get; set; }

        public string Type { get; set; }
    }
}
