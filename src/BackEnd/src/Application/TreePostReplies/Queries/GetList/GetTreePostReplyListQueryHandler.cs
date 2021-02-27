namespace GrowATree.Application.TreePostReplies.Queries.GetList
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreePostReplies;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetTreePostReplyListQueryHandler : IRequestHandler<GetTreePostReplyListQuery, TreePostReplyListModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetTreePostReplyListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TreePostReplyListModel> Handle(GetTreePostReplyListQuery request, CancellationToken cancellationToken)
        {
            var list = await this.context.TreePostReplies
                   .Skip(request.PerPage * (request.Page - 1))
                   .Take(request.PerPage)
                   .Where(x => !x.IsDeleted)
                   .OrderByDescending(x => x.CreatedOn)
                   .ProjectTo<TreePostReplyModel>(this.mapper.ConfigurationProvider)
                   .ToListAsync();

            var totalItems = list.Count;
            var meta = new Pagination
            {
                CurrentPage = request.Page,
                PerPage = request.PerPage,
                TotalItems = totalItems,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalItems / Convert.ToDouble(request.PerPage))),
            };

            var result = new TreePostReplyListModel
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
