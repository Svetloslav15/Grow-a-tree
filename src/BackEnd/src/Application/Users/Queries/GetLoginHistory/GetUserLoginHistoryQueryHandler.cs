namespace GrowATree.Application.Users.Queries.GetLoginHistory
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.LoginHistory;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetUserLoginHistoryQueryHandler : IRequestHandler<GetUserLoginHistoryQuery, LoginHistoryModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;

        public GetUserLoginHistoryQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
        {
            this.context = context;
            this.mapper = mapper;
            this.identityService = identityService;
        }

        public async Task<LoginHistoryModel> Handle(GetUserLoginHistoryQuery request, CancellationToken cancellationToken)
        {
            var userId = await this.identityService.GetCurrentUserId();
            var list = await this.context.LoginHistory
               .Where(x => x.UserId == userId)
               .Skip(request.PerPage * (request.Page - 1))
               .Take(request.PerPage)
               .ProjectTo<UserLoginModel>(this.mapper.ConfigurationProvider)
               .ToListAsync();

            var totalLogins = list.Count;
            var meta = new Pagination
            {
                CurrentPage = request.Page,
                PerPage = request.PerPage,
                TotalItems = totalLogins,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalLogins / Convert.ToDouble(request.PerPage))),
            };

            var result = new LoginHistoryModel
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
