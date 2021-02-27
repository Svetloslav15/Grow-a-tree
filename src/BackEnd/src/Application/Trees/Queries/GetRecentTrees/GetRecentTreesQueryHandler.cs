namespace GrowATree.Application.Trees.Queries.GetRecentTrees
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.Trees;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    class GetRecentTreesQueryHandler : IRequestHandler<GetRecentTreesQuery, TreeListModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetRecentTreesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TreeListModel> Handle(GetRecentTreesQuery request, CancellationToken cancellationToken)
        {
            var list = await this.context.Trees
                .OrderByDescending(tree => tree.PlantedOn)
                .Skip(request.PerPage * (request.Page - 1))
                .Take(request.PerPage)
                .ProjectTo<TreeModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            var totalTrees = list.Count;
            var meta = new Pagination
            {
                CurrentPage = request.Page,
                PerPage = request.PerPage,
                TotalItems = totalTrees,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalTrees / Convert.ToDouble(request.PerPage))),
            };

            var result = new TreeListModel
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
