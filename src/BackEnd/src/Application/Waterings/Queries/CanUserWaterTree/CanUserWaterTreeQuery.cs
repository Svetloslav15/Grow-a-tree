namespace GrowATree.Application.Waterings.Queries.CanUserWaterTree
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class CanUserWaterTreeQuery : IRequest<Result<bool>>
    {
        public string TreeId { get; set; }

        public string UserId { get; set; }
    }
}
