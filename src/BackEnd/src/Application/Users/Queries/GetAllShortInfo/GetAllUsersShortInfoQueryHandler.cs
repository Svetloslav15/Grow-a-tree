namespace GrowATree.Application.Users.Queries.GetAllShortInfo
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

    public class GetAllUsersShortInfoQueryHandler : IRequestHandler<GetAllUsersShortInfoQuery, UsersListShortInfoModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetAllUsersShortInfoQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<UsersListShortInfoModel> Handle(GetAllUsersShortInfoQuery request, CancellationToken cancellationToken)
        {
            var list = await this.context.Users
                .Skip(request.PerPage * (request.Page - 1))
                .Take(request.PerPage)
                .ProjectTo<UserShortInfoModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            var totalUsers = await this.context.Users.CountAsync();
            var meta = new Pagination
            {
                CurrentPage = request.Page,
                PerPage = request.PerPage,
                TotalItems = totalUsers,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalUsers / Convert.ToDouble(request.PerPage))),
            };

            var result = new UsersListShortInfoModel
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
