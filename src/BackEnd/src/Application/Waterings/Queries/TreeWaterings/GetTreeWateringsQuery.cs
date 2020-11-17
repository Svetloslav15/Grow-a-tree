namespace GrowATree.Application.Waterings.Queries.TreeWaterings
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.Waterings;
    using MediatR;

    public class GetTreeWateringsQuery : PagedQuery, IRequest<WateringListModel>
    {
        public string TreeId { get; set; }
    }
}
