namespace GrowATree.Application.Auth.Commands.Register
{
    using global::Application.Models.Auth.ViewModels;
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class RegisterCommand : IRequest<Result<TokenModel>>
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string City { get; set; }
    }
}
