namespace GrowATree.Application.Trees.Queries.GetShortInfoById
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.Trees;
    using GrowATree.Application.Stores.Queries.GetById;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetTreeShortInfoQueryHandler : IRequestHandler<GetTreeShortInfoQuery, Result<TreeShortInfoModel>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetTreeShortInfoQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Result<TreeShortInfoModel>> Handle(GetTreeShortInfoQuery request, CancellationToken cancellationToken)
        {
            var entity = await this.context.Trees
                .ProjectTo<TreeShortInfoModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
            {
                return Result<TreeShortInfoModel>.Failure(ErrorMessages.TreeNotFoundErrorMessage);
            }

            return Result<TreeShortInfoModel>.Success(entity);
        }
    }
}
