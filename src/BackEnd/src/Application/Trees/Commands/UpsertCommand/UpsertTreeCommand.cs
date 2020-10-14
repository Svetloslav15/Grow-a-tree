namespace GrowATree.Application.Trees.Commands.UpsertCommand
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using global::Common.Constants;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class UpsertTreeCommand : IRequest<Result<string>>
    {
        [Required(ErrorMessage = ErrorMessages.NicknameRequiredErrorMessage)]
        [MinLength(Constants.NicknameMinLength, ErrorMessage = ErrorMessages.NicknameMinLengthErrorMessage)]
        [MaxLength(Constants.NicknameMaxLength, ErrorMessage = ErrorMessages.NicknameMaxLengthErrorMessage)]
        public string Nickname { get; set; }

        public string Type { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string City { get; set; }

        public string Category { get; set; }

        public string OwnerId { get; set; }

        public List<IFormFile> ImageFiles { get; set; }
    }
}
