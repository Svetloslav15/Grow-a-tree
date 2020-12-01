namespace GrowATree.Application.TreeReactions.Queries.GetTreeReactionsByType
{
    using System.Collections.Generic;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.TreeReactions;
    using MediatR;

    public class GetTreeReactionsByTypeForTreeCommand : IRequest<Result<ICollection<TreeReactionTypeModel>>>
    {
        public string TreeId { get; set; }
    }
}
