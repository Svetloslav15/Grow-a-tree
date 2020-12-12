namespace GrowATree.Application.TreePostReactions.Queries.GetList
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreePostReactions;
    using GrowATree.Domain.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetTreePostReactionListQueryHandler : IRequestHandler<GetTreePostReactionListQuery, TreePostReactionListModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetTreePostReactionListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TreePostReactionListModel> Handle(GetTreePostReactionListQuery request, CancellationToken cancellationToken)
        {

            var reactions = this.context.TreePostReactions
                .Where(x => x.PostId == request.PostId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Type))
            {
                reactions = reactions
                    .Where(x => x.Type == (ReactionType)Enum.Parse(typeof(ReactionType), request.Type));
            }

            var list = await reactions
                .ProjectTo<TreePostReactionModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            var totalReports = list.Count;
            var meta = new Pagination
            {
                CurrentPage = request.Page,
                PerPage = request.PerPage,
                TotalItems = totalReports,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalReports / Convert.ToDouble(request.PerPage))),
            };

            var result = new TreePostReactionListModel
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
