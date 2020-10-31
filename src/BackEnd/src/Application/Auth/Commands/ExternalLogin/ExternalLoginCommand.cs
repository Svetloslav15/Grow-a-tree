namespace GrowATree.Application.Auth.Commands.FacebookLogin
{
    using System.ComponentModel.DataAnnotations;
    using global::Application.Models.Auth;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class ExternalLoginCommand : IRequest<Result<TokenModel>>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = ErrorMessages.EmailRequiredErrorMessage)]
        [EmailAddress(ErrorMessage = ErrorMessages.EmailInvalidErrorMessage)]
        public string Email { get; set; }

        public string ProviderKey { get; set; }

        public string ProviderName { get; set; }
    }
}
