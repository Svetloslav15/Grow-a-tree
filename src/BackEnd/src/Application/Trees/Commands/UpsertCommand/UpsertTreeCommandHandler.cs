namespace GrowATree.Application.Trees.Commands.UpsertCommand
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using global::Common.Interfaces;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using GrowATree.Domain.Enums;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UpsertTreeCommandHandler : IRequestHandler<UpsertTreeCommand, Result<string>>
    {
        private readonly IApplicationDbContext context;
        private readonly ICloudinaryService cloudinaryService;
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identityService;

        public UpsertTreeCommandHandler(IApplicationDbContext context, ICloudinaryService cloudinaryService, UserManager<User> userManager, IIdentityService identityService)
        {
            this.context = context;
            this.cloudinaryService = cloudinaryService;
            this.userManager = userManager;
            this.identityService = identityService;
        }

        public async Task<Result<string>> Handle(UpsertTreeCommand request, CancellationToken cancellationToken)
        {
            if (request.Id != null)
            {
                var model = await this.context.Trees
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (model == null)
                {
                    return Result<string>.Failure(ErrorMessages.TreeNotFoundErrorMessage);
                }

                if ((await this.identityService.GetCurrentUserId()) != model.OwnerId)
                {
                    return Result<string>.Failure(ErrorMessages.NotAllowedErrorMessage);
                }

                var treeWithNickname = await this.context.Trees.FirstOrDefaultAsync(x => x.Nickname == request.Nickname);
                if (treeWithNickname != null && treeWithNickname != model)
                {
                    return Result<string>.Failure(ErrorMessages.TreeNicknameInUseErrorMessage);
                }

                model.Nickname = request.Nickname;
                model.Latitude = request.Latitude.Value;
                model.Longitude = request.Longitude.Value;
                model.Type = request.Type;
                model.City = request.City;
                model.Category = request.Category;

                await this.context.SaveChangesAsync(cancellationToken);
                return Result<string>.Success(model.Id);
            }
            else
            {
                var owner = await this.userManager.FindByIdAsync(request.OwnerId);

                if (owner == null)
                {
                    return Result<string>.Failure(ErrorMessages.UserNotFoundErrorMessage);
                }

                if (request.ImageFiles == null || request.ImageFiles.Count == 0)
                {
                    return Result<string>.Failure(ErrorMessages.TreeImageRequiredErrorMessage);
                }

                if (await this.context.Trees.AnyAsync(x => x.Nickname == request.Nickname))
                {
                    return Result<string>.Failure(ErrorMessages.TreeNicknameInUseErrorMessage);
                }

                var treeModel = new Tree
                {
                    Nickname = request.Nickname,
                    Latitude = request.Latitude.Value,
                    Longitude = request.Longitude.Value,
                    Type = request.Type,
                    City = request.City,
                    Owner = owner,
                    OwnerId = owner.Id,
                    Category = request.Category,
                    PlantedOn = DateTime.Now,
                    Status = TreeStatus.GoodCondition,
                };

                foreach (var file in request.ImageFiles)
                {
                    if (!this.cloudinaryService.IsFileValid(file))
                    {
                        return Result<string>.Failure(ErrorMessages.TreeImageInvalidFormatErrorMessage);
                    }

                    var imageUrl = await this.cloudinaryService.UploudAsync(file);
                    var newImageModel = new TreeImage
                    {
                        Url = imageUrl,
                        Tree = treeModel,
                        TreeId = treeModel.Id,
                    };

                    treeModel.Images.Add(newImageModel);
                }

                var addedTree = await this.context.Trees.AddAsync(treeModel);
                await this.context.SaveChangesAsync(cancellationToken);

                return Result<string>.Success(addedTree.Entity.Id);
            }
        }
    }
}
