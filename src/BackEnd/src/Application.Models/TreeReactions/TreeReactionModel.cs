namespace GrowATree.Application.Models.TreeReactions
{
    using System;
    using GrowATree.Application.Common.Mappings;
    using GrowATree.Domain.Entities;
    using GrowATree.Domain.Enums;

    public class TreeReactionModel : IMapFrom<TreeReaction>
    {
        public string Id { get; set; }

        public ReactionType Type { get; set; }

        public string UserUserName { get; set; }

        public string UserProfilePictureUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
