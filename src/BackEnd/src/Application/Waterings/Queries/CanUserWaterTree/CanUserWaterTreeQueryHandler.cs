namespace GrowATree.Application.Waterings.Queries.CanUserWaterTree
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

    public class CanUserWaterTreeQueryHandler : IRequestHandler<CanUserWaterTreeQuery, Result<bool>>
    {
        private readonly IApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public CanUserWaterTreeQueryHandler(IApplicationDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<Result<bool>> Handle(CanUserWaterTreeQuery request, CancellationToken cancellationToken)
        {
            if (!await this.context.Trees.AnyAsync(x => x.Id == request.TreeId))
            {
                return Result<bool>.Failure(ErrorMessages.TreeNotFoundErrorMessage);
            }

            var user = await this.userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return Result<bool>.Failure(ErrorMessages.UserNotFoundErrorMessage);
            }

            var lastUserWatering = await this.context.TreeWaterings
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.WateredOn)
                .FirstOrDefaultAsync();

            var canWater = lastUserWatering == null || lastUserWatering.WateredOn.AddMinutes(Constants.UserDelayToWaterTreeInMinutes) < DateTime.Now;

            if (!canWater)
            {
                return Result<bool>.Success(false);
            }

            return Result<bool>.Success(true);
        }
    }
}
