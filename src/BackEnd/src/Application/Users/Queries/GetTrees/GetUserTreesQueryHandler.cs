namespace GrowATree.Application.Users.Queries.GetTrees
{
    using System;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreeImages;
    using GrowATree.Application.Models.Trees;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetUserTreesQueryHandler : IRequestHandler<GetUserTreesQuery, TreeListModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetUserTreesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TreeListModel> Handle(GetUserTreesQuery request, CancellationToken cancellationToken)
        {
            var list = await this.context.Users
                .Where(x => x.Id == request.Id)
                .Skip(request.PerPage * (request.Page - 1))
                .Take(request.PerPage)
                .Select(x => x.Trees)
                .Select(x => x)
                .ProjectTo<TreeModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            var totalTrees = await this.context.Users.CountAsync();
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
