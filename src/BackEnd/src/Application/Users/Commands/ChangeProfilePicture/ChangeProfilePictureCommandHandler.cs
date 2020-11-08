namespace GrowATree.Application.Users.Commands.ChangeProfilePicture
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using global::Common.Interfaces;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class ChangeProfilePictureCommandHandler : IRequestHandler<ChangeProfilePictureCommand, Result<string>>
    {
        private readonly UserManager<User> userManager;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IIdentityService identityService;

        public ChangeProfilePictureCommandHandler(UserManager<User> userManager, ICloudinaryService cloudinaryService, IIdentityService identityService)
        {
            this.userManager = userManager;
            this.cloudinaryService = cloudinaryService;
            this.identityService = identityService;
        }

        public async Task<Result<string>> Handle(ChangeProfilePictureCommand request, CancellationToken cancellationToken)
        {
            if (!this.cloudinaryService.IsFileValid(request.ProfilePictureFile))
            {
                return Result<string>.Failure(ErrorMessages.InvalidProfilePictureFormatErrorMessage);
            }

            if ((await this.identityService.GetCurrentUserId()) != request.Id)
            {
                return Result<string>.Failure(ErrorMessages.NotAllowedErrorMessage);
            }

            var uploadedImageUrl = await this.cloudinaryService.UploudAsync(request.ProfilePictureFile);

            var currentUser = await this.userManager.FindByIdAsync(request.Id);
            currentUser.ProfilePictureUrl = uploadedImageUrl == null ? currentUser.ProfilePictureUrl : uploadedImageUrl;
            var result = await this.userManager.UpdateAsync(currentUser);

            return Result<string>.Success(uploadedImageUrl);
        }
    }
}
