namespace GrowATree.Application.Users.Commands.Edit
{
    using System.ComponentModel.DataAnnotations;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.Users;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class EditUserCommand : IRequest<Result<UserModel>>
    {
        public string Id { get; set; }

        [MinLength(Constants.NameMinLength, ErrorMessage = ErrorMessages.FirstNameMinLengthErrorMessage)]
        [MaxLength(Constants.NameMaxLength, ErrorMessage = ErrorMessages.UsernameMaxLengthErrorMessage)]
        public string FirstName { get; set; }

        [MinLength(Constants.NameMinLength, ErrorMessage = ErrorMessages.LastNameMinLengthErrorMessage)]
        [MaxLength(Constants.NameMaxLength, ErrorMessage = ErrorMessages.LastNameMaxLengthErrorMessage)]
        public string LastName { get; set; }

        [Required(ErrorMessage = ErrorMessages.UsernameRequiredErrorMessage)]
        [MinLength(Constants.UsernameMinLength, ErrorMessage = ErrorMessages.UsernameMinLengthErrorMessage)]
        [MaxLength(Constants.UsernameMaxLength, ErrorMessage = ErrorMessages.UsernameMaxLengthErrorMessage)]
        public string Username { get; set; }

        [Required(ErrorMessage = ErrorMessages.CityRequiredErrorMessage)]
        public string City { get; set; }

        [RegularExpression(Constants.PhoneNumberRegEx, ErrorMessage = ErrorMessages.PhoneNumberFormatErrorMessage)]
        public string PhoneNumber { get; set; }

        public IFormFile ProfilePictureFile { get; set; }
    }
}
