namespace GrowATree.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TreePost
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Content { get; set; }

        public string TreeId { get; set; }

        public Tree Tree { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<TreePostReaction> Reactions { get; set; } = new List<TreePostReaction>();
    }
}