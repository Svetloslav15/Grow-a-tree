namespace GrowATree.Application.Trees.Queries.GetClosestTrees
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using global::Common.Constants;
    using global::Common.Interfaces;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.Trees;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetClosestTreesShortInfoQueryHandler : IRequestHandler<GetClosestTreesShortInfoQuery, TreeListShortInfoModel>
    {
        private readonly IApplicationDbContext context;
        private readonly ILocationsService locationsService;
        private readonly IMapper mapper;

        public GetClosestTreesShortInfoQueryHandler(IApplicationDbContext context, ILocationsService locationsService, IMapper mapper)
        {
            this.context = context;
            this.locationsService = locationsService;
            this.mapper = mapper;
        }

        public async Task<TreeListShortInfoModel> Handle(GetClosestTreesShortInfoQuery request, CancellationToken cancellationToken)
        {
            // OPTIMIZE THIS BULLSHIT!!!!
            var init = this.context.Trees
                .ProjectTo<TreeShortInfoModel>(this.mapper.ConfigurationProvider);

            var list = init
                .ToList()
                .Where(x => this.locationsService.CalculateDistanceBetweenTwoPoints(request.Latitude, request.Longtitude, x.Latitude, x.Longitude) <= Constants.MaxDistanceForClosestTreesInMetres)
                .OrderBy(x => this.locationsService.CalculateDistanceBetweenTwoPoints(request.Latitude, request.Longtitude, x.Latitude, x.Longitude))
                .Skip(request.PerPage * (request.Page - 1))
                .Take(request.PerPage)
                .ToList();

            foreach (var tree in list)
            {
                tree.MetresAway = double.Parse(this.locationsService.CalculateDistanceBetweenTwoPoints(request.Latitude, request.Longtitude, tree.Latitude, tree.Longitude).ToString("F0"));
            }

            var totalTrees = list.Count;
            var meta = new Pagination
            {
                CurrentPage = request.Page,
                PerPage = request.PerPage,
                TotalItems = totalTrees,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalTrees / Convert.ToDouble(request.PerPage))),
            };

            var result = new TreeListShortInfoModel
            {
                Data = list,
                Meta = new PaginationMeta
                {
                    Pagination = meta,
                },
            };

            return result;
        }
    }
}
