namespace GrowATree.Application.Users.Commands.ToggleLock
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class ToggleLockUserAccountCommand : IRequest<Result<bool>>
    {
        public string UserId { get; set; }
    }
}
