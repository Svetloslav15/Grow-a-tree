namespace GrowATree.Application.Auth.Commands.ChangeEmail
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class ChangeEmailCommand : IRequest<Result<bool>>
    {
        public string OldEmail { get; set; }

        public string NewEmail { get; set; }
    }
}