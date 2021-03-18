namespace GrowATree.Application.Trees.Queries.GetList
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

    public class GetTreeListQueryHandler : IRequestHandler<GetTreeListQuery, TreeListModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetTreeListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TreeListModel> Handle(GetTreeListQuery request, CancellationToken cancellationToken)
        {
            var list = await this.context.Trees
                .Where(x => x.IsDeleted == false)
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
