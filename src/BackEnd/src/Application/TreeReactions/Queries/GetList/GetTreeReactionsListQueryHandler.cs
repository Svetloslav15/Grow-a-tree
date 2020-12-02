namespace GrowATree.Application.TreeReactions.Queries.GetList
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreeReactions;
    using GrowATree.Domain.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetTreeReactionsListQueryHandler : IRequestHandler<GetTreeReactionsListQuery, TreeReactionListModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetTreeReactionsListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TreeReactionListModel> Handle(GetTreeReactionsListQuery request, CancellationToken cancellationToken)
        {
            var reactions = this.context.TreeReactions
                .Where(x => x.TreeId == request.TreeId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Type))
            {
                reactions = reactions
                    .Where(x => x.Type == (ReactionType)Enum.Parse(typeof(ReactionType), request.Type));
            }

            var list = await reactions
                .ProjectTo<TreeReactionModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            var totalReports = list.Count;
            var meta = new Pagination
            {
                CurrentPage = request.Page,
                PerPage = request.PerPage,
                TotalItems = totalReports,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalReports / Convert.ToDouble(request.PerPage))),
            };

            var result = new TreeReactionListModel
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
