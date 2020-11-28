namespace GrowATree.Application.Models.TreeReactions
{
    using GrowATree.Domain.Enums;

    public class TreeReactionModel
    {
        public string Id { get; set; }

        public ReactionType Type { get; set; }

        public string UserUserName { get; set; }
    }
}
