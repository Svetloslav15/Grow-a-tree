namespace GrowATree.Application.Users.Queries.GetTrees
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreeImages;
    using GrowATree.Application.Models.Trees;
    using MediatR;

    public class GetUserTreesQuery : PagedQuery, IRequest<TreeListModel>
    {
        public string Id { get; set; }
    }
}
