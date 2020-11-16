namespace GrowATree.Application.Trees.Queries.GetRandomImages
{
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.TreeImages;
    using MediatR;

    public class GetRandomTreesImagesQuery : IRequest<TreeImageListModel>
    {
    }
}
