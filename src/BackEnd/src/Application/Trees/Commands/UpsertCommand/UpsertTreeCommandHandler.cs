namespace GrowATree.Application.Trees.Commands.UpsertCommand
{
    using System;
    using System.Data.Entity;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using global::Common.Interfaces;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using GrowATree.Domain.Enums;
    using MediatR;

    public class UpsertTreeCommandHandler : IRequestHandler<UpsertTreeCommand, Result<string>>
    {
        private readonly IApplicationDbContext context;
        private readonly ICloudinaryService cloudinaryService;

        public UpsertTreeCommandHandler(IApplicationDbContext context, ICloudinaryService cloudinaryService)
        {
            this.context = context;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<Result<string>> Handle(UpsertTreeCommand request, CancellationToken cancellationToken)
        {
            var owner = await this.context.Users
                .FirstOrDefaultAsync(x => x.Id == request.OwnerId);

            if (owner == null)
            {
                return Result<string>.Failure(ErrorMessages.UserNotFoundErrorMessage);
            }

            if (request.ImageFiles.Count == 0)
            {
                return Result<string>.Failure(ErrorMessages.TreeImageRequiredErrorMessage);
            }

            if (await this.context.Trees.AnyAsync(x => x.Nickname == request.Nickname))
            {
                return Result<string>.Failure(ErrorMessages.NicknameInUseErrorMessage);
            }

            var treeModel = new Tree
            {
                Nickname = request.Nickname,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
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
