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

    public class GetTreeReactionsByTypeForTreeCommandHandler : IRequestHandler<GetTreeReactionsByTypeForTreeCommand, Result<ICollection<TreeReactionTypeModel>>>
    {
        private readonly IApplicationDbContext context;

        public GetTreeReactionsByTypeForTreeCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<ICollection<TreeReactionTypeModel>>> Handle(GetTreeReactionsByTypeForTreeCommand request, CancellationToken cancellationToken)
        {
            var reactionTypes = await this.context.TreeReactions
                .Where(x => x.TreeId == request.TreeId)
                .GroupBy(x => x.Type)
                .Select(x => new TreeReactionTypeModel
                {
                    Type = x.Key.ToString(),
                    ReactionsCount = x.Count(),
                })
                .OrderByDescending(x => x.ReactionsCount)
                .ToListAsync();

            return Result<ICollection<TreeReactionTypeModel>>.Success(reactionTypes);
        }
    }
}
