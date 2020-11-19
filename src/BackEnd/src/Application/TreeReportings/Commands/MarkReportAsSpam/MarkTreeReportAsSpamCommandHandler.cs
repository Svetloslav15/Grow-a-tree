namespace GrowATree.Application.TreeReportings.Commands.MarkReportAsSpam
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class MarkTreeReportAsSpamCommandHandler : IRequestHandler<MarkTreeReportAsSpamCommand, Result<bool>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIdentityService identityService;
        private readonly UserManager<User> userManager;

        public MarkTreeReportAsSpamCommandHandler(IApplicationDbContext context, IIdentityService identityService, UserManager<User> userManager)
        {
            this.context = context;
            this.identityService = identityService;
            this.userManager = userManager;
        }

        public async Task<Result<bool>> Handle(MarkTreeReportAsSpamCommand request, CancellationToken cancellationToken)
        {
            if (!await this.context.TreeReports.AnyAsync(x => x.Id == request.TreeReportId))
            {
                return Result<bool>.Failure(ErrorMessages.TreeReporNotFoundErrorMessage);
            }

            var report = await this.context.TreeReports
                .Include(x => x.Tree)
                .FirstAsync(x => x.Id == request.TreeReportId);

            if (report.Tree.OwnerId != await this.identityService.GetCurrentUserId())
            {
                return Result<bool>.Failure(ErrorMessages.NotAllowedErrorMessage);
            }

            report.IsSpam = true;
            await this.context.SaveChangesAsync(CancellationToken.None);

            return Result<bool>.Success(true);
        }
    }
}
