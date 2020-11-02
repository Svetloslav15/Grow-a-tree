namespace GrowATree.Application.Users.Queries.GetAllShortInfo
{
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.Users;
    using MediatR;

    public class GetAllUsersShortInfoQuery : PagedQuery, IRequest<UserListShortInfoModel>
    {
    }
}
