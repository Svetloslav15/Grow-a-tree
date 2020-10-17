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

        public UpsertTreeCommandHandler(IApplicationDbContext context, ICloudinaryService cloudinaryService, UserManager<User> userManager)
        {
            this.context = context;
            this.cloudinaryService = cloudinaryService;
            this.userManager = userManager;
        }

        public async Task<Result<string>> Handle(UpsertTreeCommand request, CancellationToken cancellationToken)
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
