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
        public string Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.TreeNicknameRequiredErrorMessage)]
        [MinLength(Constants.TreeNicknameMinLength, ErrorMessage = ErrorMessages.TreeNicknameMinLengthErrorMessage)]
        [MaxLength(Constants.TreeNicknameMaxLength, ErrorMessage = ErrorMessages.TreeNicknameMaxLengthErrorMessage)]
        public string Nickname { get; set; }

        [Required(ErrorMessage = ErrorMessages.TreeTypeRequiredErrorMessage)]
        public string Type { get; set; }

        [Required(ErrorMessage = ErrorMessages.TreeLocationRequiredErrorMessage)]
        public double? Latitude { get; set; }

        [Required(ErrorMessage = ErrorMessages.TreeLocationRequiredErrorMessage)]
        public double? Longitude { get; set; }

        [Required(ErrorMessage = ErrorMessages.CityRequiredErrorMessage)]
        public string City { get; set; }

        [Required(ErrorMessage = ErrorMessages.TreeCategoryRequiredErrorMessage)]
        public string Category { get; set; }

        public string OwnerId { get; set; }

        public List<IFormFile> ImageFiles { get; set; }
    }
}
