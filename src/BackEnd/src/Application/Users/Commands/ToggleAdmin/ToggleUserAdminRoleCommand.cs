namespace GrowATree.Application.Users.Commands.ToggleAdmin
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class ToggleUserAdminRoleCommand : IRequest<Result<bool>>
    {
        public string UserId { get; set; }
    }
}
