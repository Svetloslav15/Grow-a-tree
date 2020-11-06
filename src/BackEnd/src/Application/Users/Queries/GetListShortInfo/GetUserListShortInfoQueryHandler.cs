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

    public class GetUserListShortInfoQueryHandler : IRequestHandler<GetUserListShortInfoQuery, UserListShortInfoModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetUserListShortInfoQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<UserListShortInfoModel> Handle(GetUserListShortInfoQuery request, CancellationToken cancellationToken)
        {
            var list = await this.context.Users
                .Skip(request.PerPage * (request.Page - 1))
                .Take(request.PerPage)
                .ProjectTo<UserShortInfoModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            var totalUsers = list.Count;
            var meta = new Pagination
            {
                CurrentPage = request.Page,
                PerPage = request.PerPage,
                TotalItems = totalUsers,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalUsers / Convert.ToDouble(request.PerPage))),
            };

            var result = new UserListShortInfoModel
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
