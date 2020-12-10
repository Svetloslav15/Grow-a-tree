namespace GrowATree.Application.TreePostReactions.Queries.GetTypes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.TreeReactions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetTreePostReactionTypesQueryHandler : IRequestHandler<GetTreePostReactionTypesQuery, Result<IList<ReactionTypeModel>>>
    {
        private readonly IApplicationDbContext context;

        public GetTreePostReactionTypesQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<IList<ReactionTypeModel>>> Handle(GetTreePostReactionTypesQuery request, CancellationToken cancellationToken)
        {
            var reactionTypes = await this.context.TreePostReactions
                .Where(x => x.PostId == request.PostId)
                .GroupBy(x => x.Type)
                .Select(x => new ReactionTypeModel
                {
                    Type = x.Key.ToString(),
                    ReactionsCount = x.Count(),
                })
                .ToListAsync();

            var result = reactionTypes
                .OrderByDescending(x => x.ReactionsCount)
                .ToList();

            return Result<IList<ReactionTypeModel>>.Success(result);
        }
    }
}
