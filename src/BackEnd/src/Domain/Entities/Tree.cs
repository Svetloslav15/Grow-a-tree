namespace GrowATree.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using GrowATree.Domain.Enums;

    public class Tree
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Nickname { get; set; }

        public string Type { get; set; }

        public DateTime PlantedOn { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public TreeStatus Status { get; set; }

        public string City { get; set; }

        public string Category { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<TreeImage> Images { get; set; } = new HashSet<TreeImage>();

        public ICollection<TreeReaction> Reactions { get; set; } = new List<TreeReaction>();

        public ICollection<TreeReport> Reports { get; set; } = new List<TreeReport>();

        public ICollection<TreeWatering> Waterings { get; set; } = new List<TreeWatering>();

        public ICollection<TreePost> Posts { get; set; } = new List<TreePost>();
    }
}