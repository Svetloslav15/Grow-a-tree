namespace GrowATree.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using GrowATree.Domain.Enums;

    public class TreePostReaction
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public ReactionType Type { get; set; }

        public DateTime CreatedOn { get; set; }

        public string PostId { get; set; }

        public TreePost Post { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}