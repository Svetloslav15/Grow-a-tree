namespace GrowATree.Application.Models.TreePostReactions
{
    using System;
    using GrowATree.Application.Common.Mappings;
    using GrowATree.Domain.Entities;
    using GrowATree.Domain.Enums;

    public class TreePostReactionModel : IMapFrom<TreePostReaction>
    {
        public string Id { get; set; }

        public ReactionType Type { get; set; }

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
