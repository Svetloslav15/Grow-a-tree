﻿namespace GrowATree.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Url { get; set; }

        public string TreeId { get; set; }

        public Tree Tree { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }
    }
}