namespace GrowATree.Application.Waterings.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class WaterTreeCommandHandler : IRequestHandler<WaterTreeCommand, Result<bool>>
    {
        private readonly IApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public WaterTreeCommandHandler(IApplicationDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<Result<bool>> Handle(WaterTreeCommand request, CancellationToken cancellationToken)
        {
            if (!(await this.context.Trees.AnyAsync(x => x.Id == request.TreeId)))
            {
                return Result<bool>.Failure(ErrorMessages.TreeNotFoundErrorMessage);
            }

            var user = await this.userManager.FindByIdAsync(request.WatererId);
            if (user == null)
            {
                return Result<bool>.Failure(ErrorMessages.UserNotFoundErrorMessage);
            }

            var lastUserWatering = await this.context.TreeWaterings
                .Where(x => x.UserId == request.WatererId)
                .OrderByDescending(x => x.WateredOn)
                .FirstOrDefaultAsync();

            var canWater = lastUserWatering == null || lastUserWatering.WateredOn.AddMinutes(Constants.UserDelayToWaterTreeInMinutes) < DateTime.Now;

            if (!canWater)
            {
                return Result<bool>.Failure(ErrorMessages.WateringDelayErrorMessage);
            }

            var newWatering = new TreeWatering
            {
                TreeId = request.TreeId,
                UserId = request.WatererId,
                WateredOn = DateTime.Now,
            };

            await this.context.TreeWaterings.AddAsync(newWatering);
            await this.context.SaveChangesAsync(CancellationToken.None);

            return Result<bool>.Success(true);
        }
    }
}
