namespace GrowATree.Application.Auth.Commands.Register
{
    using System.ComponentModel.DataAnnotations;
    using global::Application.Models.Auth.ViewModels;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using MediatR;

    /// <summary>
    /// Model for register user input.
    /// </summary>
    public class RegisterCommand : IRequest<Result<TokenModel>>
    {
        [Required(ErrorMessage = ErrorMessages.EmailRequiredErrorMessage)]
        [EmailAddress(ErrorMessage = ErrorMessages.EmailInvalidErrorMessage)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessages.UsernameRequiredErrorMessage)]
        [MinLength(Constants.UsernameMinLength, ErrorMessage = ErrorMessages.UsernameMinLengthErrorMessage)]
        [MaxLength(Constants.UsernameMaxLength, ErrorMessage = ErrorMessages.UsernameMaxLengthErrorMessage)]
        public string Username { get; set; }

        [Required(ErrorMessage = ErrorMessages.PasswordRequiredErrorMessage)]
        [MinLength(Constants.PasswordMinLength, ErrorMessage = ErrorMessages.PasswordMinLengthErrorMessage)]
        [MaxLength(Constants.PasswordMaxLength, ErrorMessage = ErrorMessages.PasswordMaxLengthErrorMessage)]
        public string Password { get; set; }

        [Required(ErrorMessage = ErrorMessages.CityRequiredErrorMessage)]
        public string City { get; set; }
    }
}
