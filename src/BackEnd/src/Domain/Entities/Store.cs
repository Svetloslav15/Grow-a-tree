using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GrowATree.Domain.Entities
{
    public class Store
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Email { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string WorkingTime { get; set; }

        public string Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

        public ICollection<PromoCode> PromoCodes { get; set; } = new List<PromoCode>();
    }
}