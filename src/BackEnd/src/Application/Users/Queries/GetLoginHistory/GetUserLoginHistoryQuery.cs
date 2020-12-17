namespace GrowATree.Application.Users.Queries.GetLoginHistory
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.LoginHistory;
    using MediatR;

    public class GetUserLoginHistoryQuery : PagedQuery, IRequest<LoginHistoryModel>
    {
    }
}
