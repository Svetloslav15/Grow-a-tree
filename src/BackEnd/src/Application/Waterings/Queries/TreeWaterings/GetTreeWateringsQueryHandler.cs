namespace GrowATree.Application.Waterings.Queries.TreeWaterings
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.Waterings;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetTreeWateringsQueryHandler : IRequestHandler<GetTreeWateringsQuery, WateringListModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetTreeWateringsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<WateringListModel> Handle(GetTreeWateringsQuery request, CancellationToken cancellationToken)
        {
            if (!await this.context.Trees.AnyAsync(x => x.Id == request.TreeId))
            {
                return WateringListModel.Failure<WateringListModel>(ErrorMessages.TreeNotFoundErrorMessage);
            }

            var list = await this.context.TreeWaterings
                .Where(x => x.TreeId == request.TreeId)
                .OrderByDescending(x => x.WateredOn)
                .Skip(request.PerPage * (request.Page - 1))
                .Take(request.PerPage)
                .ProjectTo<WateringModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            var totalTrees = list.Count;
            var meta = new Pagination
            {
                CurrentPage = request.Page,
                PerPage = request.PerPage,
                TotalItems = totalTrees,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalTrees / Convert.ToDouble(request.PerPage))),
            };

            var result = new WateringListModel
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
