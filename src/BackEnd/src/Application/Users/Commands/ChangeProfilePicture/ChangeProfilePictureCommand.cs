namespace GrowATree.Application.Users.Commands.ChangeProfilePicture
{
    using System.ComponentModel.DataAnnotations;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class ChangeProfilePictureCommand : IRequest<Result<string>>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.ProfilePictureRequiredErrorMessage)]
        public IFormFile ProfilePictureFile { get; set; }
    }
}
