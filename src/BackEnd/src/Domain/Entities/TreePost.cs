using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GrowATree.Domain.Entities
{
    public class TreePost
    {
        [Key]
        public string Id { get; set; }

        public string Content { get; set; }

        public string TreeId { get; set; }

        public Tree Tree { get; set; }

        public ICollection<TreePostReaction> Reactions { get; set; } = new List<TreePostReaction>();
    }
}