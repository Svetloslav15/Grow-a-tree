namespace GrowATree.Application.Trees.Queries.GetById
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

    public class GetTreeByIdQueryHandler : IRequestHandler<GetTreeByIdQuery, Result<TreeModel>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetTreeByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Result<TreeModel>> Handle(GetTreeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await this.context.Trees
                .ProjectTo<TreeModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
            {
                return Result<TreeModel>.Failure(ErrorMessages.TreeNotFoundErrorMessage);
            }

            return Result<TreeModel>.Success(entity);
        }
    }
}
