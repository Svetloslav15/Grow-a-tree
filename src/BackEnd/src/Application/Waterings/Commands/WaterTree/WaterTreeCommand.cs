namespace GrowATree.Application.Waterings.Commands
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class WaterTreeCommand : IRequest<Result<bool>>
    {
        public string TreeId { get; set; }

        public string WatererId { get; set; }
    }
}
