using GrowATree.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GrowATree.Domain.Entities
{
    public class PromoCode
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; }

        public int DiscountValue { get; set; }

        public PromoCodeType Type { get; set; }

        public int UsedCount { get; set; } = 0;

        public int UsesLeft { get; set; }

        public bool IsActive { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string StoreId { get; set; }

        public Store Store { get; set; }
    }
}