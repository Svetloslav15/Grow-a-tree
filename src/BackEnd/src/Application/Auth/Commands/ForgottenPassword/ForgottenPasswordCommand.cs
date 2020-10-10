namespace GrowATree.Application.Auth.Commands.ForgottenPassword
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class ForgottenPasswordCommand : IRequest<Result<bool>>
    {
        public string Email { get; set; }
    }
}