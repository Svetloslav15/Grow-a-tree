using GrowATree.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GrowATree.Domain.Entities
{
    public class TreePostReaction
    {
        [Key]
        public string Id { get; set; }

        public ReactionType Type { get; set; }

        public string PostId { get; set; }

        public TreePost Post { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}