namespace GrowATree.Application.Trees.Commands.DeleteImage
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class DeleteTreeImageCommandHandler : IRequestHandler<DeleteTreeImageCommand, Result<string>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIdentityService identityService;
        private readonly UserManager<User> userManager;

        public DeleteTreeImageCommandHandler(IApplicationDbContext context, IIdentityService identityService, UserManager<User> userManager)
        {
            this.context = context;
            this.identityService = identityService;
            this.userManager = userManager;
        }

        public async Task<Result<string>> Handle(DeleteTreeImageCommand request, CancellationToken cancellationToken)
        {
            var treeImageModel = await this.context.TreeImages
                .FirstOrDefaultAsync(x => x.Id == request.ImageId);

            if (treeImageModel == null)
            {
                return Result<string>.Failure(ErrorMessages.TreeImageNotFoundErrorMessage);
            }


            if ((await this.identityService.GetCurrentUserId()) != treeImageModel.Tree.OwnerId || await this.userManager.IsInRoleAsync(await this.userManager.FindByIdAsync(await this.identityService.GetCurrentUserId()),
                Constants.AdminRoleName))
            {
                treeImageModel.IsDeleted = true;
                treeImageModel.DeletedOn = DateTime.Now;
                await this.context.SaveChangesAsync(cancellationToken);

                return Result<string>.Success(treeImageModel.Id);
            }

            return Result<string>.Failure(ErrorMessages.NotAllowedErrorMessage);
        }
    }
}
