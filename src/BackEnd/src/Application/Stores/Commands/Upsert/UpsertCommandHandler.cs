namespace GrowATree.Application.Stores.Commands.Upsert
{
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class UpsertCommandHandler : IRequestHandler<UpsertCommand, Result<bool>>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;

        public UpsertCommandHandler(IApplicationDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<Result<bool>> Handle(UpsertCommand request, CancellationToken cancellationToken)
        {
            Store entity;
            if (request.Id != null)
            {
                var store = await this.dbContext.Stores.FindAsync(request.Id);
                var storeUser = await this.userManager.FindByIdAsync(store.ApplicationUserId);
                if (store != null && storeUser != null)
                {
                    // Update properties for store
                    store.Latitude = request.Latitude;
                    store.Longitude = request.Longitute;
                    store.Description = request.Description;
                    store.WorkingHours = request.WorkingHours;

                    // Update properties for the app user for the store
                    storeUser.City = request.City;
                    storeUser.Email = request.Email;
                    storeUser.UserName = request.Name;

                    // Update user
                    await this.userManager.UpdateAsync(storeUser);

                    return Result<bool>.Success(true);
                }
                else
                {
                    return Result<bool>.Failure(ErrorMessages.StoreNotFound);
                }
            }
            else
            {
                // First, we create the application user
                var appUser = new User
                {
                    City = request.City,
                    Email = request.Email,
                    UserName = request.Name,
                    PhoneNumber = request.PhoneNumber,
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                };

                var user = await this.userManager.CreateAsync(appUser, request.Password);
                await this.userManager.AddToRoleAsync(appUser, Constants.StoreRoleName);

                if (user.Succeeded == true)
                {
                    entity = new Store
                    {
                        Latitude = request.Latitude,
                        Longitude = request.Longitute,
                        Description = request.Description,
                        WorkingHours = request.WorkingHours,
                        ApplicationUserId = appUser.Id,
                        ApplicationUser = appUser,
                    };

                    this.dbContext.Stores.Add(entity);
                }
                else
                {
                    return Result<bool>.Failure(ErrorMessages.AccountFailureErrorMessage);
                }
            }

            await this.dbContext.SaveChangesAsync(CancellationToken.None);

            return Result<bool>.Success(true);
        }
    }
}
