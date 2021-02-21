namespace GrowATree.Application.TreePosts.Queries.GetList
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreePosts;
    using MediatR;

    public class GetTreePostListQuery : PagedQuery, IRequest<TreePostListModel>
    {
        public string TreeId { get; set; }
    }
}
