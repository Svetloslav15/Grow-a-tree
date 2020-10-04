namespace GrowATree.Application.Auth.Commands.ConfirmEmail
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class ConfirmEmailCommand : IRequest<Result<bool>>
    {
        public string Token { get; set; }

        public string Email { get; set; }
    }
}