namespace GrowATree.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    public class TreePostReply
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string TreePostId { get; set; }

        public TreePost TreePost { get; set; }

        public ICollection<TreePostReplyReaction> Reactions { get; set; } = new HashSet<TreePostReplyReaction>();
    }
}
