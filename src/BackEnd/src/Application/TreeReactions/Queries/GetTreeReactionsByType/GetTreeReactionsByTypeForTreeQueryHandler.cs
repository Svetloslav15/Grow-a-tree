namespace GrowATree.Application.TreeReactions.Queries.GetTreeReactionsByType
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

    public class GetTreeReactionsByTypeForTreeQueryHandler : IRequestHandler<GetTreeReactionsByTypeForTreeQuery, Result<ICollection<ReactionTypeModel>>>
    {
        private readonly IApplicationDbContext context;

        public GetTreeReactionsByTypeForTreeQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<ICollection<ReactionTypeModel>>> Handle(GetTreeReactionsByTypeForTreeQuery request, CancellationToken cancellationToken)
        {
            var reactionTypes = await this.context.TreeReactions
                .Where(x => x.TreeId == request.TreeId)
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

            return Result<ICollection<ReactionTypeModel>>.Success(result);
        }
    }
}
