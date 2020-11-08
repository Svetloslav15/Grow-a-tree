namespace GrowATree.Application.Users.Commands.Edit
{
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Interfaces;
    using global::Common.Constants;
    using global::Common.Interfaces;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.Users;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, Result<UserModel>>
    {
        private readonly IApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IIdentityService identityService;

        public EditUserCommandHandler(IApplicationDbContext context, UserManager<User> userManager, ICloudinaryService cloudinaryService, IIdentityService identityService)
        {
            this.context = context;
            this.userManager = userManager;
            this.cloudinaryService = cloudinaryService;
            this.identityService = identityService;
        }

        public async Task<Result<UserModel>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {

            if ((await this.identityService.GetCurrentUserId()) != request.Id)
            {
                return Result<UserModel>.Failure(ErrorMessages.NotAllowedErrorMessage);
            }

            var currentUser = await this.context.Users
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (currentUser != null)
            {
                var userWithUsername = await this.userManager.Users.FirstOrDefaultAsync(x => x.UserName == request.Username);
                if (userWithUsername != null && userWithUsername != currentUser)
                {
                    return Result<UserModel>.Failure(ErrorMessages.UsernameInUseErrorMessage);
                }

                currentUser.City = request.City;
                currentUser.FirstName = request.FirstName;
                currentUser.LastName = request.LastName;
                currentUser.PhoneNumber = request.PhoneNumber;
                currentUser.UserName = request.Username;

                var result = await this.userManager.UpdateAsync(currentUser);

                if (result.Succeeded)
                {
                    var resultModel = new UserModel
                    {
                        Id = currentUser.Id,
                        City = currentUser.City,
                        Email = currentUser.Email,
                        PhoneNumber = currentUser.PhoneNumber,
                        ProfilePictureUrl = currentUser.ProfilePictureUrl,
                        UserName = currentUser.UserName,
                    };

                    return Result<UserModel>.Success(resultModel);
                }
                else
                {
                    return Result<UserModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
                }
            }
            else
            {
                return Result<UserModel>.Failure(ErrorMessages.UserNotFoundErrorMessage);
            }
        }
    }
}
