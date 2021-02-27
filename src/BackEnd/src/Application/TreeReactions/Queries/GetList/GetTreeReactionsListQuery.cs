namespace GrowATree.Application.TreeReactions.Queries.GetList
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreeReactions;
    using MediatR;

    public class GetTreeReactionsListQuery : PagedQuery, IRequest<TreeReactionListModel>
    {
        public string TreeId { get; set; }

        public string Type { get; set; }
    }
}
