namespace GrowATree.Application.Models.TreeImages
{
    using GrowATree.Application.Common.Mappings;
    using GrowATree.Domain.Entities;

    public class TreeImageModel : IMapFrom<TreeImage>
    {
        public string Id { get; set; }

        public string Url { get; set; }
    }
}
