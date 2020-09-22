using GrowATree.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GrowATree.Domain.Entities
{
    public class ReactionImage
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public ReactionType ReactionType { get; set; }
    }
}