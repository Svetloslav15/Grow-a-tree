namespace GrowATree.Application.Trees.Queries.GetDeletedImages
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreeImages;
    using MediatR;

    public class GetTreeDeletedImagesQuery : PagedQuery, IRequest<TreeImageListModel>
    {
        public string Id { get; set; }
    }
}
