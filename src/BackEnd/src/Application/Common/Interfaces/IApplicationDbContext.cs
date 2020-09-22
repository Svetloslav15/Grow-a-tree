using System.Threading;
using System.Threading.Tasks;

namespace GrowATree.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
