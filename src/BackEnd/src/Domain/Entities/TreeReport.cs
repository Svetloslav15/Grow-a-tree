namespace GrowATree.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using GrowATree.Domain.Enums;

    public class TreeReport
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Message { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }

        public bool IsSpam { get; set; }

        public TreeReportType Type { get; set; }

        public string TreeId { get; set; }

        public Tree Tree { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}