namespace GrowATree.Application.Auth.Commands.FacebookLogin
{
    using global::Application.Models.Auth;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class ExternalLoginCommand : IRequest<Result<TokenModel>>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }

        public string ProviderName { get; set; }

        public string ProviderKey { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
