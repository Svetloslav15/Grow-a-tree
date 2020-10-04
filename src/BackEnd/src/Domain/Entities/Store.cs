namespace GrowATree.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Store
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string WorkingHours { get; set; }

        public string Description { get; set; }

        public string ApplicationUserId { get; set; }

        public User ApplicationUser { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

        public ICollection<PromoCode> PromoCodes { get; set; } = new List<PromoCode>();
    }
}