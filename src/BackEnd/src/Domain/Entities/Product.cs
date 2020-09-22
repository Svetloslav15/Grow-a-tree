using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrowATree.Domain.Entities
{
    public class Product
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(38, 20)")]
        public decimal Price { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();

        public string StoreId { get; set; }

        public Store Store { get; set; }
    }
}