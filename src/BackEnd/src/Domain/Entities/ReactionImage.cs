using GrowATree.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GrowATree.Domain.Entities
{
    public class ReactionImage
    {
        [Key]
        public string Id { get; set; }

        public ReactionType ReactionType { get; set; }
    }
}