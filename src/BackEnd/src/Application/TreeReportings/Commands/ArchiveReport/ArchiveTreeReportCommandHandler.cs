namespace GrowATree.Application.TreeReportings.Commands.ArchiveReport
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class ArchiveTreeReportCommandHandler : IRequestHandler<ArchiveTreeReportCommand, Result<bool>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIdentityService identityService;

        public ArchiveTreeReportCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }

        public async Task<Result<bool>> Handle(ArchiveTreeReportCommand request, CancellationToken cancellationToken)
        {
            var tree = await this.context.Trees
                .Include(x => x.Reports)
                .FirstOrDefaultAsync(x => x.Id == request.TreeId);

            if (tree.OwnerId != await this.identityService.GetCurrentUserId())
            {
                return Result<bool>.Failure(ErrorMessages.NotAllowedErrorMessage);
            }

            var reports = tree
                .Reports
                .Where(x => x.IsActive == true && x.Type == (TreeReportType)Enum.Parse(typeof(TreeReportType), request.ReportType))
                .ToList();

            reports
                .ForEach(x => x.IsActive = false);
            await this.context.SaveChangesAsync(CancellationToken.None);

            return Result<bool>.Success(true);
        }
    }
}
