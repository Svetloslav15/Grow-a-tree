namespace GrowATree.Application.Models.TreePosts
{
    using GrowATree.Application.Common.Mappings;
    using GrowATree.Domain.Entities;

    public class TreePostModel : IMapFrom<TreePost>
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string UserUserName { get; set; }

        public string UserProfilePictureUrl { get; set; }

        public string UserId { get; set; }
    }
}
