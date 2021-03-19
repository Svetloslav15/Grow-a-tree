namespace GrowATree.Application.Users.Commands.ToggleAdmin
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class ToggleUserAdminRoleCommandHandler : IRequestHandler<ToggleUserAdminRoleCommand, Result<bool>>
    {
        private readonly IApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public ToggleUserAdminRoleCommandHandler(IApplicationDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<Result<bool>> Handle(ToggleUserAdminRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await this.context
                .Users
                .FirstOrDefaultAsync(x => x.Id == request.UserId);

            var isAdmin = await this.userManager.IsInRoleAsync(user, Constants.AdminRoleName);
            if (isAdmin)
            {
                await this.userManager.RemoveFromRoleAsync(user, Constants.AdminRoleName);
            }
            else
            {
                await this.userManager.AddToRoleAsync(user, Constants.AdminRoleName);
            }

            return Result<bool>.Success(true);
        }
    }
}
