namespace GrowATree.Infrastructure.Persistence
{
    using System.Data;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Domain.Common;
    using GrowATree.Domain.Entities;
    using IdentityServer4.EntityFramework.Options;
    using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.Extensions.Options;

    public class ApplicationDbContext : ApiAuthorizationDbContext<User>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private IDbContextTransaction _currentTransaction;

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

        public DbSet<TreePostReply> TreePostReplies { get; set; }

        public DbSet<TreePostReplyReaction> TreePostReplyReactions { get; set; }

        public DbSet<LoginHistory> LoginHistory { get; set; }

        public DbSet<UnknownTrees> UnknownTrees { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options, operationalStoreOptions)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await base.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
