namespace GrowATree.Application.TreePostReplyReactions.Queries.GetList
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreePostReplyReactions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetTreePostReplyReactionListQueryHandler : IRequestHandler<GetTreePostReplyReactionListQuery, TreePostReplyReactionListModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetTreePostReplyReactionListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TreePostReplyReactionListModel> Handle(GetTreePostReplyReactionListQuery request, CancellationToken cancellationToken)
        {
            var list = await this.context.TreePostReplyReactions
                  .Skip(request.PerPage * (request.Page - 1))
                  .Take(request.PerPage)
                  .OrderByDescending(x => x.CreatedOn)
                  .ProjectTo<TreePostReplyReactionModel>(this.mapper.ConfigurationProvider)
                  .ToListAsync();

            var totalReactions = list.Count;
            var meta = new Pagination
            {
                CurrentPage = request.Page,
                PerPage = request.PerPage,
                TotalItems = totalReactions,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalReactions / Convert.ToDouble(request.PerPage))),
            };

            var result = new TreePostReplyReactionListModel
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
