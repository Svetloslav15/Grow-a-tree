namespace GrowATree.Application.TreePosts.Queries.GetList
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreePosts;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetTreePostListQueryHandler : IRequestHandler<GetTreePostListQuery, TreePostListModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetTreePostListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TreePostListModel> Handle(GetTreePostListQuery request, CancellationToken cancellationToken)
        {
            var list = await this.context.TreePosts
                   .Skip(request.PerPage * (request.Page - 1))
                   .Take(request.PerPage)
                   .OrderByDescending(x => x.CreatedOn)
                   .ProjectTo<TreePostModel>(this.mapper.ConfigurationProvider)
                   .ToListAsync();

            var totalTrees = list.Count;
            var meta = new Pagination
            {
                CurrentPage = request.Page,
                PerPage = request.PerPage,
                TotalItems = totalTrees,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalTrees / Convert.ToDouble(request.PerPage))),
            };

            var result = new TreePostListModel
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
