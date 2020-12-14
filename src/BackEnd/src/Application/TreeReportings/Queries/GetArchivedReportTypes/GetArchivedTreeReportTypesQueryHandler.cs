namespace GrowATree.Application.TreeReportings.Queries.GetArchivedReportTypes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.TreeReporting;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetArchivedTreeReportTypesQueryHandler : IRequestHandler<GetArchivedTreeReportTypesQuery, Result<ICollection<TreeReportTypeModel>>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetArchivedTreeReportTypesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Result<ICollection<TreeReportTypeModel>>> Handle(GetArchivedTreeReportTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await this.context.TreeReports
                 .Where(x => x.TreeId == request.TreeId && x.IsActive == false && x.IsSpam == false)
                 .GroupBy(x => x.Type)
                 .Select(x => new TreeReportTypeModel
                 {
                     Type = x.Key.ToString(),
                     ReportsCount = x.Count(),
                 })
                 .ToListAsync();

            return Result<ICollection<TreeReportTypeModel>>.Success(types);
        }
    }
}
