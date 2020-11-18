namespace GrowATree.Application.TreeReportings.Commands.ReportTree
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using global::Common.Interfaces;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class ReportTreeCommandHandler : IRequestHandler<ReportTreeCommand, Result<bool>>
    {
        private readonly IApplicationDbContext context;
        private readonly ICloudinaryService cloudinaryService;
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identityService;

        public ReportTreeCommandHandler(
            IApplicationDbContext context,
            ICloudinaryService cloudinaryService,
            UserManager<User> userManager,
            IIdentityService identityService)
        {
            this.context = context;
            this.cloudinaryService = cloudinaryService;
            this.userManager = userManager;
            this.identityService = identityService;
        }

        public async Task<Result<bool>> Handle(ReportTreeCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == request.TreeId)
            {
                return Result<bool>.Failure(ErrorMessages.TreeReportSelfReportErrorMessage);
            }

            if (!this.cloudinaryService.IsFileValid(request.ImageFile))
            {
                return Result<bool>.Failure(ErrorMessages.TreeImageInvalidFormatErrorMessage);
            }

            if (!await this.context.Trees.AnyAsync(x => x.Id == request.TreeId))
            {
                return Result<bool>.Failure(ErrorMessages.TreeNotFoundErrorMessage);
            }

            var user = await this.userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return Result<bool>.Failure(ErrorMessages.UserNotFoundErrorMessage);
            }

            var duplicateReport = await this.context.TreeReports
                .AnyAsync(x => x.TreeId == request.TreeId
                && x.UserId == request.UserId
                && x.Type == request.Type
                && x.IsActive == true
                && x.IsSpam == false);

            if (duplicateReport)
            {
                return Result<bool>.Failure(ErrorMessages.TreeReportDublicateErrorMessage);
            }

            var newReport = new TreeReport
            {
                TreeId = request.TreeId,
                UserId = request.UserId,
                IsActive = true,
                IsSpam = false,
                ImageUrl = await this.cloudinaryService.UploudAsync(request.ImageFile),
                Message = request.Message,
                Type = request.Type,
            };

            await this.context.TreeReports.AddAsync(newReport);
            await this.context.SaveChangesAsync(CancellationToken.None);

            return Result<bool>.Success(true);
        }
    }
}
