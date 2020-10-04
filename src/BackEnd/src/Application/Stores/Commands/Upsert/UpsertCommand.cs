namespace GrowATree.Application.Stores.Commands.Upsert
{
    using System.ComponentModel.DataAnnotations;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class UpsertCommand : IRequest<Result<bool>>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.EmailRequiredErrorMessage)]
        [EmailAddress(ErrorMessage = ErrorMessages.EmailInvalidErrorMessage)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessages.PasswordRequiredErrorMessage)]
        [MinLength(Constants.PasswordMinLength, ErrorMessage = ErrorMessages.PasswordMinLengthErrorMessage)]
        [MaxLength(Constants.PasswordMaxLength, ErrorMessage = ErrorMessages.PasswordMaxLengthErrorMessage)]
        public string Password { get; set; }

        [Required(ErrorMessage = ErrorMessages.UsernameRequiredErrorMessage)]
        [MinLength(Constants.UsernameMinLength, ErrorMessage = ErrorMessages.UsernameMinLengthErrorMessage)]
        [MaxLength(Constants.UsernameMaxLength, ErrorMessage = ErrorMessages.UsernameMaxLengthErrorMessage)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.CoordinatesRequiredErrorMessage)]
        public string Latitude { get; set; }

        [Required(ErrorMessage = ErrorMessages.CoordinatesRequiredErrorMessage)]
        public string Longitute { get; set; }

        [Required(ErrorMessage = ErrorMessages.CityRequiredErrorMessage)]
        public string City { get; set; }

        [Required(ErrorMessage = ErrorMessages.WorkingHoursRequiredErrorMessage)]
        public string WorkingHours { get; set; }

        [Required(ErrorMessage = ErrorMessages.DescriptionRequiredErrorMessage)]
        public string Description { get; set; }

        [Required(ErrorMessage = ErrorMessages.PhoneNumberRequiredErrorMessage)]
        public string PhoneNumber { get; set; }
    }
}
