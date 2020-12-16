namespace GrowATree.Application.Users.Queries.GetReferrers
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
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class GetUserReferralListQueryHandler : IRequestHandler<GetUserReferralListQuery, UserListShortInfoModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public GetUserReferralListQueryHandler(IApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<UserListShortInfoModel> Handle(GetUserReferralListQuery request, CancellationToken cancellationToken)
        {
            if (await this.userManager.FindByIdAsync(request.Id) == null)
            {
                return UserListShortInfoModel.Failure<UserListShortInfoModel>(ErrorMessages.UserNotFoundErrorMessage);
            }

            var list = await this.context.Users
               .Where(x => x.Id == request.Id)
               .SelectMany(x => x.Referals)
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

            var result = new UserListShortInfoModel()
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
