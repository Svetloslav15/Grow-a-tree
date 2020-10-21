namespace GrowATree.Application.Users.Queries.GetShortInfoById
{
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.Users;
    using MediatR;

    public class GetUserShortInfoQuery : IRequest<Result<UserShortInfoModel>>
    {
        public string Id { get; set; }
    }
}
