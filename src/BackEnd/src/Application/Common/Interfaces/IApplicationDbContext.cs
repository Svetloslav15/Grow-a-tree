namespace GrowATree.Application.Common.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using GrowATree.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<TreeImage> TreeImages { get; set; }

        public DbSet<TreeReaction> TreeReactions { get; set; }

        public DbSet<Tree> Trees { get; set; }

        public DbSet<TreePost> TreePosts { get; set; }

        public DbSet<TreePostReaction> TreePostReactions { get; set; }

        public DbSet<TreeReport> TreeReports { get; set; }

        public DbSet<TreeWatering> TreeWaterings { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<PromoCode> PromoCodes { get; set; }

        public DbSet<Store> Stores { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
