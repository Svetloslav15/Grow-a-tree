using GrowATree.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GrowATree.Domain.Entities
{
    public class Reaction
    {
        [Key]
        public string Id { get; set; }

        public ReactionType Type { get; set; }

        public string TreeId { get; set; }

        public Tree Tree { get; set; }

        public string UserId { get; set; }
        
        public User User { get; set; }
    }
}