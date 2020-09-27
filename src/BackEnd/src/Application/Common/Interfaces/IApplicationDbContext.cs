namespace GrowATree.Application.Common.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
