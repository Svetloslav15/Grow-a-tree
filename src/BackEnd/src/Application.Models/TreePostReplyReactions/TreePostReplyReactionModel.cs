namespace GrowATree.Application.Models.TreePostReplyReactions
{
    using System;
    using GrowATree.Application.Common.Mappings;
    using GrowATree.Domain.Entities;
    using GrowATree.Domain.Enums;

    public class TreePostReplyReactionModel : IMapFrom<TreePostReplyReaction>
    {
        public string Id { get; set; }

        public ReactionType Type { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public string TreePostReplyId { get; set; }
    }
}
