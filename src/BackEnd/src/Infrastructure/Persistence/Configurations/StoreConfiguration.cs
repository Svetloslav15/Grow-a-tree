using GrowATree.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrowATree.Infrastructure.Persistence.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasMany(x => x.Products)
                .WithOne(x => x.Store)
                .HasForeignKey(x => x.StoreId);

            builder.HasMany(x => x.PromoCodes)
                .WithOne(x => x.Store)
                .HasForeignKey(x => x.StoreId);
        }
    }
}