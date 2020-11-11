namespace GrowATree.Application.Users.Queries.UserNearTree
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using global::Common.Interfaces;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class IsUserNearTreeQueryHandler : IRequestHandler<IsUserNearTreeQuery, Result<bool>>
    {
        private readonly IApplicationDbContext context;
        private readonly ILocationsService locationsService;

        public IsUserNearTreeQueryHandler(IApplicationDbContext context, ILocationsService locationsService)
        {
            this.context = context;
            this.locationsService = locationsService;
        }

        public async Task<Result<bool>> Handle(IsUserNearTreeQuery request, CancellationToken cancellationToken)
        {
            var tree = await this.context.Trees
                .FirstOrDefaultAsync(x => x.Id == request.TreeId);

            if (tree == null)
            {
                return Result<bool>.Failure(ErrorMessages.TreeNotFoundErrorMessage);
            }

            var distanceBetweenTreeAndUser = this.locationsService.CalculateDistanceBetweenTwoPoints(tree.Latitude, tree.Longitude, request.Latitude, request.Longitude);

            if (distanceBetweenTreeAndUser <= Constants.MaxDistanceBetweenTreeAndUser)
            {
                return Result<bool>.Success(true);
            }
            else
            {
                return Result<bool>.Success(false);
            }
        }
    }
}
