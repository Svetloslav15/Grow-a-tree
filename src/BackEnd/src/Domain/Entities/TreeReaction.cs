namespace GrowATree.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using GrowATree.Domain.Enums;

    public class TreeReaction
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public ReactionType Type { get; set; }

        public string TreeId { get; set; }

        public Tree Tree { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}