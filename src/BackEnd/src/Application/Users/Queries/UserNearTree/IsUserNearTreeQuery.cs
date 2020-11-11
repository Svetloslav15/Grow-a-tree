namespace GrowATree.Application.Users.Queries.UserNearTree
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class IsUserNearTreeQuery : IRequest<Result<bool>>
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string TreeId { get; set; }
    }
}
