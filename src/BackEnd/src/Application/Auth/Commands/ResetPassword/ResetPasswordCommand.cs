namespace GrowATree.Application.Auth.Commands.ResetPassword
{
    using System.ComponentModel.DataAnnotations;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class ResetPasswordCommand : IRequest<Result<bool>>
    {
        [EmailAddress(ErrorMessage = ErrorMessages.EmailInvalidErrorMessage)]
        public string Email { get; set; }

        public string Token { get; set; }

        [MinLength(Constants.PasswordMinLength, ErrorMessage = ErrorMessages.PasswordMinLengthErrorMessage)]
        [MaxLength(Constants.PasswordMaxLength, ErrorMessage = ErrorMessages.PasswordMaxLengthErrorMessage)]
        public string Password { get; set; }
    }
}