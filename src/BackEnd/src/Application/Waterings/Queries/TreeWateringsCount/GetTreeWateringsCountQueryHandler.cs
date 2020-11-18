namespace GrowATree.Application.Waterings.Queries.TreeWateringsCount
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetTreeWateringsCountQueryHandler : IRequestHandler<GetTreeWateringsCountQuery, Result<int>>
    {
        private readonly IApplicationDbContext context;

        public GetTreeWateringsCountQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<int>> Handle(GetTreeWateringsCountQuery request, CancellationToken cancellationToken)
        {
            if (!await this.context.Trees.AnyAsync(x => x.Id == request.TreeId))
            {
                return Result<int>.Failure(ErrorMessages.TreeNotFoundErrorMessage);
            }

            var count = await this.context.TreeWaterings
                .Where(x => x.TreeId == request.TreeId)
                .CountAsync();

            return Result<int>.Success(count);
        }
    }
}
