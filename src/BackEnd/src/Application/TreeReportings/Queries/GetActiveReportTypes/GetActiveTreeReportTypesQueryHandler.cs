namespace GrowATree.Application.TreeReportings.Queries.GetActiveReportTypes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.TreeReporting;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetActiveTreeReportTypesQueryHandler : IRequestHandler<GetActiveTreeReportTypesQuery, Result<ICollection<TreeReportTypeModel>>>
    {
        private readonly IApplicationDbContext context;

        public GetActiveTreeReportTypesQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<ICollection<TreeReportTypeModel>>> Handle(GetActiveTreeReportTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await this.context.TreeReports
                .Where(x => x.TreeId == request.TreeId && x.IsActive == true && x.IsSpam == false)
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
