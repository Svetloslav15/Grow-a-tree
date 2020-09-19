using GrowATree.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrowATree.Infrastructure.Persistence.Configurations
{
    public class TreePostConfiguration : IEntityTypeConfiguration<TreePost>
    {
        public void Configure(EntityTypeBuilder<TreePost> builder)
        {
            builder.HasMany(x => x.Reactions)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId);
        }
    }
}