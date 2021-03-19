namespace GrowATree.Application.Users.Queries.GetAll
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.Users;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using GrowATree.Domain.Entities;
    using System.Collections.Generic;

    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, UserListModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public GetUserListQueryHandler(IApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<UserListModel> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var list = await this.context.Users
                .Skip(request.PerPage * (request.Page - 1))
                .Take(request.PerPage)
                .ProjectTo<UserModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            var listUsers = await this.context.Users
                .Skip(request.PerPage * (request.Page - 1))
                .Take(request.PerPage)
                .ToListAsync();

            for (int index = 0; index < listUsers.Count; index++)
            {
                list[index].IsAdmin = await this.userManager.IsInRoleAsync(listUsers[index], Constants.AdminRoleName);
            }

            var totalUsers = list.Count;
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
