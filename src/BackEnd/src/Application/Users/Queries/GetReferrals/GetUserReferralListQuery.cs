namespace GrowATree.Application.Users.Queries.GetReferrers
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.Users;
    using MediatR;

    public class GetUserReferralListQuery : PagedQuery, IRequest<UserListShortInfoModel>
    {
        public string Id { get; set; }
    }
}
