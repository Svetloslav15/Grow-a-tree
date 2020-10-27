namespace GrowATree.Application.Users.Queries.GetShortInfoById
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.Users;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetUserShortInfoQueryHandler : IRequestHandler<GetUserShortInfoQuery, Result<UserShortInfoModel>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetUserShortInfoQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Result<UserShortInfoModel>> Handle(GetUserShortInfoQuery request, CancellationToken cancellationToken)
        {
            var entity = await this.context.Users
                .ProjectTo<UserShortInfoModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
            {
                return Result<UserShortInfoModel>.Failure(ErrorMessages.UserNotFoundErrorMessage);
            }

            return Result<UserShortInfoModel>.Success(entity);
        }
    }
}
