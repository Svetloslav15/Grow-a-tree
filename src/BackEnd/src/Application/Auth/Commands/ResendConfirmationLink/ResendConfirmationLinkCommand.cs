namespace GrowATree.Application.Auth.Commands
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class ResendConfirmationLinkCommand : IRequest<Result<bool>>
    {
        public string Email { get; set; }
    }
}