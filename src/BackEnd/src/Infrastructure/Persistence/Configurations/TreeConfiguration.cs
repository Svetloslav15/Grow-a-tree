using GrowATree.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrowATree.Infrastructure.Persistence.Configurations
{
    public class TreeConfiguration : IEntityTypeConfiguration<Tree>
    {
        public void Configure(EntityTypeBuilder<Tree> builder)
        {
            builder.HasMany(x => x.Reactions)
                .WithOne(x => x.Tree)
                .HasForeignKey(x => x.TreeId);

            builder.HasMany(x => x.Reports)
                .WithOne(x => x.Tree)
                .HasForeignKey(x => x.TreeId);

            builder.HasMany(x => x.Waterings)
                .WithOne(x => x.Tree)
                .HasForeignKey(x => x.TreeId);

            builder.HasMany(x => x.Posts)
                .WithOne(x => x.Tree)
                .HasForeignKey(x => x.TreeId);

            builder.HasMany(x => x.Images)
                .WithOne(x => x.Tree)
                .HasForeignKey(x => x.TreeId);
        }
    }
}