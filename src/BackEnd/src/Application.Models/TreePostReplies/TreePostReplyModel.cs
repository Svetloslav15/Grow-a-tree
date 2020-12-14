namespace GrowATree.Application.Models.TreePostReplies
{
    using System;
    using GrowATree.Application.Common.Mappings;
    using GrowATree.Domain.Entities;

    public class TreePostReplyModel : IMapFrom<TreePostReply>
    {
        public string Id { get; set; }

        public string Conent { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public string UserProfilePictureUrl { get; set; }

        public string TreePostId { get; set; }
    }
}
