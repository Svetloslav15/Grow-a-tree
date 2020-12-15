namespace GrowATree.Domain.Entities
{
    using System;
    using GrowATree.Domain.Enums;

    public class TreePostReplyReaction
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public ReactionType Type { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string TreePostReplyId { get; set; }

        public TreePostReply TreePostReply { get; set; }
    }
}
