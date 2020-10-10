namespace GrowATree.Application.Auth.Commands.RefreshToken
{
    using System.ComponentModel.DataAnnotations;
    using global::Application.Models.Auth;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class RefreshTokenCommand : IRequest<Result<TokenModel>>
    {
        [Required(ErrorMessage = ErrorMessages.AccessTokenInvalidErrorMessage)]
        public string AccessToken { get; set; }

        [Required(ErrorMessage = ErrorMessages.AccessTokenInvalidErrorMessage)]
        public string RefreshToken { get; set; }
    }
}
