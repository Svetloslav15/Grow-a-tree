namespace GrowATree.Application.Auth.Commands
{
    using global::Application.Models.Auth;
    using GrowATree.Application.Common.Models;
    using MediatR;

    /// <summary>
    /// Model for register user input.
    /// </summary>
    public class LoginCommand : IRequest<Result<TokenModel>>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Ip { get; set; }

        public string DeviceName { get; set; }
    }
}
