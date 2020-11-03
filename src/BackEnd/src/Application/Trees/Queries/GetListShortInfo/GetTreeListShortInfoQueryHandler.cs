namespace GrowATree.Application.Trees.Queries.GetListShortInfo
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

    public class GetTreeListShortInfoQueryHandler : IRequestHandler<GetTreeListShortInfoQuery, TreeListShortInfoModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetTreeListShortInfoQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TreeListShortInfoModel> Handle(GetTreeListShortInfoQuery request, CancellationToken cancellationToken)
        {
            var list = await this.context.Trees
                .Skip(request.PerPage * (request.Page - 1))
                .Take(request.PerPage)
                .ProjectTo<TreeShortInfoModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            var totalTrees = await this.context.Users.CountAsync();
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
