namespace GrowATree.Application.TreePostReactions.Queries.GetTypes
{
    using System.Collections.Generic;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.TreeReactions;
    using MediatR;

    public class GetTreePostReactionTypesQuery : IRequest<Result<IList<ReactionTypeModel>>>
    {
        public string PostId { get; set; }
    }
}
