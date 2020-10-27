namespace GrowATree.Application.Models.Images
{
    using GrowATree.Application.Common.Mappings;
    using GrowATree.Domain.Entities;

    public class ImageModel : IMapFrom<TreeImage>
    {
        public string Id { get; set; }

        public string Url { get; set; }
    }
}
