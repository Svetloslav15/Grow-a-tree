namespace GrowATree.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(38, 20)")]
        public decimal Price { get; set; }

        public ICollection<ProductImage> Images { get; set; } = new HashSet<ProductImage>();

        public string StoreId { get; set; }

        public Store Store { get; set; }
    }
}