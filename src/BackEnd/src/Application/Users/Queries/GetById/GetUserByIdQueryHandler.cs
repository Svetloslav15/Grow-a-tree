namespace GrowATree.Application.Users.Queries.GetById
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

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserModel>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetUserByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Result<UserModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await this.context.Users
                .ProjectTo<UserModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
            {
                return Result<UserModel>.Failure(ErrorMessages.UserNotFoundErrorMessage);
            }

            return Result<UserModel>.Success(entity);
        }
    }
}
