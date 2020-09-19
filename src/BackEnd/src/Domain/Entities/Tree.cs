using GrowATree.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GrowATree.Domain.Entities
{
    public class Tree
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string NickName { get; set; }
        
        public string Type { get; set; }

        public string PlantedAt { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public TreeStatus Status { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();

        public ICollection<TreeReport> Reports { get; set; } = new List<TreeReport>();
        
        public ICollection<TreeWatering> Waterings { get; set; } = new List<TreeWatering>();

        public ICollection<TreePost> Posts { get; set; } = new List<TreePost>();
    }
}