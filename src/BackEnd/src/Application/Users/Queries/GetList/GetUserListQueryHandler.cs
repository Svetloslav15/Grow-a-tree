namespace GrowATree.Application.Users.Queries.GetAll
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.Users;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, UserListModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetUserListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<UserListModel> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var list = await this.context.Users
                .Skip(request.PerPage * (request.Page - 1))
                .Take(request.PerPage)
                .ProjectTo<UserModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            var totalUsers = await this.context.Users.CountAsync();
            var meta = new Pagination
            {
                CurrentPage = request.Page,
                PerPage = request.PerPage,
                TotalItems = totalUsers,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalUsers / Convert.ToDouble(request.PerPage))),
            };

            var result = new UserListModel()
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
